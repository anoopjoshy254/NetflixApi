using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Payments.DTOs;

namespace NetflixApi.Modules.Payments.Interfaces
{
    public interface IPaymentService
    {
        Task<OrderResponseDto> CreateOrderAsync(int userId, decimal amount, string currency);
        Task<bool> VerifyPaymentAsync(int userId, VerifyPaymentRequestDto request);
        Task<IEnumerable<PaymentHistoryDto>> GetHistoryAsync(int userId);
        Task<InvoiceDto> GetInvoiceAsync(int invoiceId);
        Task<bool> RefundPaymentAsync(int paymentId, string reason);
    }
}
