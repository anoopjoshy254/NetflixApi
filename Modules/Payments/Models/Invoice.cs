using System;

namespace NetflixApi.Modules.Payments.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PaymentId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssuedAt { get; set; }
    }
}
