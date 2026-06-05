using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using NetflixApi.Modules.Subscription.Models;
using NetflixApi.Modules.Subscription.Repositories.Interfaces;

namespace NetflixApi.Modules.Subscription.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubscription> GetByIdAsync(int id) => await _context.UserSubscriptions.FindAsync(id);
        
        public async Task<IEnumerable<UserSubscription>> GetAllAsync() => await _context.UserSubscriptions.ToListAsync();
        
        public async Task<UserSubscription> AddAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }
    }
}
