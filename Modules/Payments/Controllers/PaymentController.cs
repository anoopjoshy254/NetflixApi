using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Payments.DTOs;
using NetflixApi.Modules.Payments.Interfaces;

namespace NetflixApi.Modules.Payments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            var userId = GetUserId();
            var order = await _paymentService.CreateOrderAsync(userId, request.Amount, request.Currency);
            return Ok(order);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyPaymentRequestDto request)
        {
            var userId = GetUserId();
            var success = await _paymentService.VerifyPaymentAsync(userId, request);
            if (!success) return BadRequest(new { message = "Payment verification failed" });
            return Ok(new { message = "Payment verified successfully" });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = GetUserId();
            var history = await _paymentService.GetHistoryAsync(userId);
            return Ok(history);
        }

        [HttpGet("invoice/{id}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await _paymentService.GetInvoiceAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpPost("refund/{paymentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RefundPayment(int paymentId, [FromBody] string reason)
        {
            var success = await _paymentService.RefundPaymentAsync(paymentId, reason);
            if (!success) return BadRequest("Unable to process refund.");
            return Ok(new { message = "Refund requested successfully" });
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }
    }
}
