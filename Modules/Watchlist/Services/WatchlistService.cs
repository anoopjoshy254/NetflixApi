using NetflixApi.Modules.Watchlist.Models;

namespace NetflixApi.Modules.Watchlist.Services
{
    public class WatchlistService : IWatchlistService
    {
        private static readonly List<WatchlistItem> _mockWatchlist = new List<WatchlistItem>();

        public Task<WatchlistItem> AddToWatchlistAsync(Guid userId, Guid videoId)
        {
            var item = _mockWatchlist.FirstOrDefault(w => w.UserId == userId && w.VideoId == videoId);
            if (item == null)
            {
                item = new WatchlistItem
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    VideoId = videoId,
                    AddedAt = DateTime.UtcNow
                };
                _mockWatchlist.Add(item);
            }
            return Task.FromResult(item);
        }

        public Task RemoveFromWatchlistAsync(Guid userId, Guid videoId)
        {
            _mockWatchlist.RemoveAll(w => w.UserId == userId && w.VideoId == videoId);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<WatchlistItem>> GetUserWatchlistAsync(Guid userId)
        {
            var list = _mockWatchlist.Where(w => w.UserId == userId).OrderByDescending(w => w.AddedAt);
            return Task.FromResult<IEnumerable<WatchlistItem>>(list);
        }
    }
}
