using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Subscription.DTOs;

namespace NetflixApi.Modules.Subscription.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionPlanDto>> GetActivePlansAsync();
        Task<bool> SubscribeAsync(Guid userId, int planId);
        Task<bool> UpgradeAsync(Guid userId, int newPlanId);
        Task<bool> DowngradeAsync(Guid userId, int newPlanId);
        Task<bool> CancelAsync(Guid userId);
        Task<UserSubscriptionDto> GetMySubscriptionAsync(Guid userId);
        Task<SubscriptionStatusDto> GetStatusAsync(Guid userId);
        Task<bool> IsActiveAsync(Guid userId);
    }
}
