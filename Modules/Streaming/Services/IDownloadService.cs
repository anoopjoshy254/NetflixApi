using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public interface IDownloadService
    {
        Task<Download> ProcessDownloadAsync(Guid userId, Guid videoId, string quality);
        Task<IEnumerable<Download>> GetUserDownloadsAsync(Guid userId);
        Task RemoveDownloadAsync(Guid userId, Guid videoId);
    }
}
