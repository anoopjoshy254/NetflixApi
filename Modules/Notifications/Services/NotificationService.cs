using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetflixApi.Modules.Notifications.DTOs;
using NetflixApi.Modules.Notifications.Interfaces;
using NetflixApi.Data;
using NetflixApi.Modules.Notifications.Models;

namespace NetflixApi.Modules.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ApplicationDbContext context, ILogger<NotificationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderBy(n => n.IsRead)
                .ThenByDescending(n => n.SentAt)
                .Select(n => new NotificationDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Message = n.Message,
                    Type = n.Type,
                    IsRead = n.IsRead,
                    SentAt = n.SentAt
                })
                .ToListAsync();

            return notifications;
        }

        public async Task<bool> MarkAsReadAsync(Guid userId, int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);

            if (notification == null) return false;

            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(Guid userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var n in notifications)
            {
                n.IsRead = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SendBillingAlertAsync(Guid userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = "Billing Alert",
                Message = message,
                Type = "InApp",
                IsRead = false,
                SentAt = DateTime.UtcNow
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task SendNewContentAlertAsync(Guid userId, string contentTitle)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = "New Content Available",
                Message = $"Check out the newly added content: {contentTitle}",
                Type = "InApp",
                IsRead = false,
                SentAt = DateTime.UtcNow
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task SendSubscriptionExpiryAlertAsync(Guid userId, int daysLeft)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = "Subscription Expiring Soon",
                Message = $"Your subscription will expire in {daysLeft} days. Please renew to continue watching.",
                Type = "Email", // or InApp
                IsRead = false,
                SentAt = DateTime.UtcNow
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
