using NetflixApi.Modules.Watchlist.Models;

namespace NetflixApi.Modules.Watchlist.Services
{
    public interface IWatchlistService
    {
        Task<WatchlistItem> AddToWatchlistAsync(Guid userId, Guid videoId);
        Task RemoveFromWatchlistAsync(Guid userId, Guid videoId);
        Task<IEnumerable<WatchlistItem>> GetUserWatchlistAsync(Guid userId);
    }
}
