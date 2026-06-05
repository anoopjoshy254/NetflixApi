using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Subscription.DTOs;

namespace NetflixApi.Modules.Subscription.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionPlanDto>> GetActivePlansAsync();
        Task<bool> SubscribeAsync(int userId, int planId);
        Task<bool> UpgradeAsync(int userId, int newPlanId);
        Task<bool> DowngradeAsync(int userId, int newPlanId);
        Task<bool> CancelAsync(int userId);
        Task<UserSubscriptionDto> GetMySubscriptionAsync(int userId);
        Task<SubscriptionStatusDto> GetStatusAsync(int userId);
        Task<bool> IsActiveAsync(int userId);
    }
}
