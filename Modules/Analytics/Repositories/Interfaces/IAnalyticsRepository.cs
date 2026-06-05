using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Analytics.Models;

namespace NetflixApi.Modules.Analytics.Repositories.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<AnalyticsEvent> GetByIdAsync(int id);
        Task<IEnumerable<AnalyticsEvent>> GetAllAsync();
        Task<AnalyticsEvent> AddAsync(AnalyticsEvent analyticsEvent);
    }
}
