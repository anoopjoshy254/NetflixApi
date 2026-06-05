using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using NetflixApi.Modules.Analytics.Models;
using NetflixApi.Modules.Analytics.Repositories.Interfaces;

namespace NetflixApi.Modules.Analytics.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsEvent> GetByIdAsync(int id) => await _context.Analytics.FindAsync(id);
        
        public async Task<IEnumerable<AnalyticsEvent>> GetAllAsync() => await _context.Analytics.ToListAsync();
        
        public async Task<AnalyticsEvent> AddAsync(AnalyticsEvent analyticsEvent)
        {
            _context.Analytics.Add(analyticsEvent);
            await _context.SaveChangesAsync();
            return analyticsEvent;
        }
    }
}
