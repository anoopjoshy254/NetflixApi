using NetflixApi.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Modules.Auth.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly ApplicationDbContext _context;

        public PasswordResetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PasswordReset?> GetByTokenAsync(string token)
        {
            return await _context.PasswordResets.FirstOrDefaultAsync(pr => pr.Token == token);
        }

        public async Task AddAsync(PasswordReset reset)
        {
            await _context.PasswordResets.AddAsync(reset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PasswordReset reset)
        {
            _context.PasswordResets.Update(reset);
            await _context.SaveChangesAsync();
        }
    }
}
