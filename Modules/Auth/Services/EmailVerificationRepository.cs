using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Modules.Auth.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailVerificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmailVerification?> GetByTokenAsync(string token)
        {
            return await _context.EmailVerifications.FirstOrDefaultAsync(ev => ev.Token == token);
        }

        public async Task AddAsync(EmailVerification verification)
        {
            await _context.EmailVerifications.AddAsync(verification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmailVerification verification)
        {
            _context.EmailVerifications.Update(verification);
            await _context.SaveChangesAsync();
        }
    }
}
