using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Notifications.DTOs;

namespace NetflixApi.Modules.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(Guid userId);
        Task<bool> MarkAsReadAsync(Guid userId, int notificationId);
        Task<bool> MarkAllAsReadAsync(Guid userId);
        
        Task SendBillingAlertAsync(Guid userId, string message);
        Task SendNewContentAlertAsync(Guid userId, string contentTitle);
        Task SendSubscriptionExpiryAlertAsync(Guid userId, int daysLeft);
    }
}
