using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Analytics.DTOs;

namespace NetflixApi.Modules.Analytics.Interfaces
{
    public interface IAnalyticsService
    {
        Task<bool> TrackEventAsync(int? userId, TrackEventRequestDto request);
        Task<IEnumerable<PopularContentDto>> GetPopularContentAsync();
        Task<WatchStatsResponseDto> GetWatchStatsAsync(int page, int pageSize);
        Task<IEnumerable<RevenueTrendDto>> GetRevenueTrendAsync();
    }
}
