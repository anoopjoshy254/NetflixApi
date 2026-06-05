using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profile>> GetProfilesByUserIdAsync(Guid userId)
        {
            return await _context.Profiles.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task AddAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Profile profile)
        {
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }
    }
}
