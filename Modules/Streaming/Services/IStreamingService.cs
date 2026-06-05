using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public interface IStreamingService
    {
        Task<Video?> GetVideoAsync(Guid videoId);
        Task UpdatePlaybackSessionAsync(Guid userId, Guid videoId, int stoppedAtSeconds);
        Task<int> GetResumePositionAsync(Guid userId, Guid videoId);
    }
}
