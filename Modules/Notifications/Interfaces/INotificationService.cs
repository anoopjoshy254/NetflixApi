using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Notifications.DTOs;

namespace NetflixApi.Modules.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(int userId);
        Task<bool> MarkAsReadAsync(int userId, int notificationId);
        Task<bool> MarkAllAsReadAsync(int userId);
        
        Task SendBillingAlertAsync(int userId, string message);
        Task SendNewContentAlertAsync(int userId, string contentTitle);
        Task SendSubscriptionExpiryAlertAsync(int userId, int daysLeft);
    }
}
