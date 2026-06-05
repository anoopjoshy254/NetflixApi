using System;

namespace NetflixApi.Modules.Payments.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
