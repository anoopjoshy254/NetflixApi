using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetflixApi.Modules.Payments.DTOs;
using NetflixApi.Modules.Payments.Interfaces;
using NetflixApi.Modules.Payments.Models;
using Razorpay.Api;
using NetflixApi.Data;
using NetflixApi.Modules.Payments.Models;

namespace NetflixApi.Modules.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly RazorpaySettings _razorpaySettings;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ApplicationDbContext context, IOptions<RazorpaySettings> razorpaySettings, ILogger<PaymentService> logger)
        {
            _context = context;
            _razorpaySettings = razorpaySettings.Value;
            _logger = logger;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(Guid userId, decimal amount, string currency)
        {
            var client = new RazorpayClient(_razorpaySettings.KeyId, _razorpaySettings.KeySecret);

            var options = new Dictionary<string, object>
            {
                { "amount", amount * 100 }, // Razorpay expects amount in subunits (paise)
                { "currency", currency },
                { "receipt", Guid.NewGuid().ToString() }
            };

            var order = client.Order.Create(options);
            string orderId = order["id"].ToString();

            var payment = new NetflixApi.Modules.Payments.Models.Payment
            {
                UserId = userId,
                Amount = amount,
                Currency = currency,
                Status = "Pending",
                RazorpayOrderId = orderId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new OrderResponseDto
            {
                OrderId = orderId,
                Amount = amount,
                Currency = currency,
                RazorpayKeyId = _razorpaySettings.KeyId
            };
        }

        public async Task<bool> VerifyPaymentAsync(Guid userId, VerifyPaymentRequestDto request)
        {
            var attributes = new Dictionary<string, string>
            {
                { "razorpay_order_id", request.RazorpayOrderId },
                { "razorpay_payment_id", request.RazorpayPaymentId },
                { "razorpay_signature", request.RazorpaySignature }
            };

            try
            {
                Utils.verifyPaymentSignature(attributes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment signature verification failed");
                return false;
            }

            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.RazorpayOrderId == request.RazorpayOrderId);
            if (payment == null) return false;

            payment.Status = "Success";
            payment.RazorpayPaymentId = request.RazorpayPaymentId;
            payment.RazorpaySignature = request.RazorpaySignature;

            var invoice = new NetflixApi.Modules.Payments.Models.Invoice
            {
                UserId = userId,
                PaymentId = payment.Id,
                InvoiceNumber = $"INV-{DateTime.UtcNow.Year}-{new Random().Next(100000, 999999)}",
                TotalAmount = payment.Amount,
                IssuedAt = DateTime.UtcNow
            };

            _context.Invoices.Add(invoice);

            var userSubscription = await _context.UserSubscriptions.FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Pending");
            if (userSubscription != null)
            {
                userSubscription.Status = "Active";
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaymentHistoryDto>> GetHistoryAsync(Guid userId)
        {
            var payments = await _context.Payments
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var invoices = await _context.Invoices
                .Where(i => i.UserId == userId)
                .ToListAsync();

            return payments.Select(p => new PaymentHistoryDto
            {
                Id = p.Id,
                Amount = p.Amount,
                Currency = p.Currency,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                InvoiceNumber = invoices.FirstOrDefault(i => i.PaymentId == p.Id)?.InvoiceNumber
            });
        }

        public async Task<InvoiceDto> GetInvoiceAsync(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null) return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                TotalAmount = invoice.TotalAmount,
                IssuedAt = invoice.IssuedAt
            };
        }

        public async Task<bool> RefundPaymentAsync(int paymentId, string reason)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null || payment.Status != "Success") return false;

            var refund = new NetflixApi.Modules.Payments.Models.Refund
            {
                PaymentId = paymentId,
                Reason = reason,
                Amount = payment.Amount,
                Status = "Requested",
                ProcessedAt = DateTime.UtcNow
            };

            _context.Refunds.Add(refund);
            payment.Status = "Refunded";
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
