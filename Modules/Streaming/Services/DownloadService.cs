using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public class DownloadService : IDownloadService
    {
        private static readonly List<Download> _mockDownloads = new List<Download>();

        public Task<Download> ProcessDownloadAsync(Guid userId, Guid videoId, string quality)
        {
            var download = new Download
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                VideoId = videoId,
                Quality = quality,
                DownloadedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };
            _mockDownloads.Add(download);
            return Task.FromResult(download);
        }

        public Task<IEnumerable<Download>> GetUserDownloadsAsync(Guid userId)
        {
            var downloads = _mockDownloads.Where(d => d.UserId == userId);
            return Task.FromResult<IEnumerable<Download>>(downloads);
        }

        public Task RemoveDownloadAsync(Guid userId, Guid videoId)
        {
            _mockDownloads.RemoveAll(d => d.UserId == userId && d.VideoId == videoId);
            return Task.CompletedTask;
        }
    }
}
