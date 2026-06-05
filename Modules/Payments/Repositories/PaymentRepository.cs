using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using NetflixApi.Modules.Payments.Models;
using NetflixApi.Modules.Payments.Repositories.Interfaces;

namespace NetflixApi.Modules.Payments.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(int id) => await _context.Payments.FindAsync(id);
        
        public async Task<IEnumerable<Payment>> GetAllAsync() => await _context.Payments.ToListAsync();
        
        public async Task<Payment> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
