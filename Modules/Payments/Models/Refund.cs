using System;

namespace NetflixApi.Modules.Payments.Models
{
    public class Refund
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
