using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public interface IHistoryService
    {
        Task AddToHistoryAsync(Guid userId, Guid videoId, bool completed);
        Task<IEnumerable<WatchHistory>> GetUserHistoryAsync(Guid userId);
        Task ClearHistoryAsync(Guid userId);
    }
}
