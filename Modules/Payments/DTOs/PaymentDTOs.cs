using System;

namespace NetflixApi.Modules.Payments.DTOs
{
    public class CreateOrderRequestDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class OrderResponseDto
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string RazorpayKeyId { get; set; }
    }

    public class VerifyPaymentRequestDto
    {
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
    }

    public class PaymentHistoryDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
