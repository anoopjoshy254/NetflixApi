using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using NetflixApi.Modules.Notifications.Models;
using NetflixApi.Modules.Notifications.Repositories.Interfaces;

namespace NetflixApi.Modules.Notifications.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> GetByIdAsync(int id) => await _context.Notifications.FindAsync(id);
        
        public async Task<IEnumerable<Notification>> GetAllAsync() => await _context.Notifications.ToListAsync();
        
        public async Task<Notification> AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
