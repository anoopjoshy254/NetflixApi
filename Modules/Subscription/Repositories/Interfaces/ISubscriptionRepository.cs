using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Subscription.Models;

namespace NetflixApi.Modules.Subscription.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<UserSubscription> GetByIdAsync(int id);
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task<UserSubscription> AddAsync(UserSubscription subscription);
    }
}
