using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public class HistoryService : IHistoryService
    {
        private static readonly List<WatchHistory> _mockHistory = new List<WatchHistory>();

        public Task AddToHistoryAsync(Guid userId, Guid videoId, bool completed)
        {
            var entry = new WatchHistory
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                VideoId = videoId,
                WatchedAt = DateTime.UtcNow,
                Completed = completed
            };
            _mockHistory.Add(entry);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<WatchHistory>> GetUserHistoryAsync(Guid userId)
        {
            var history = _mockHistory.Where(h => h.UserId == userId).OrderByDescending(h => h.WatchedAt);
            return Task.FromResult<IEnumerable<WatchHistory>>(history);
        }

        public Task ClearHistoryAsync(Guid userId)
        {
            _mockHistory.RemoveAll(h => h.UserId == userId);
            return Task.CompletedTask;
        }
    }
}
