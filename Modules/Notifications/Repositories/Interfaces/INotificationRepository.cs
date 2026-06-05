using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Notifications.Models;

namespace NetflixApi.Modules.Notifications.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> AddAsync(Notification notification);
    }
}
