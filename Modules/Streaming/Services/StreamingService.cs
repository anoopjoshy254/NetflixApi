using NetflixApi.Modules.Streaming.Models;

namespace NetflixApi.Modules.Streaming.Services
{
    public class StreamingService : IStreamingService
    {
        // Mock data for demonstration purposes
        private static readonly List<Video> _mockVideos = new List<Video>
        {
            new Video { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Title = "Mock Movie 1", Url = "mock-video-1.mp4", DurationInSeconds = 3600 }
        };

        private static readonly List<StreamingSession> _mockSessions = new List<StreamingSession>();

        public Task<Video?> GetVideoAsync(Guid videoId)
        {
            var video = _mockVideos.FirstOrDefault(v => v.Id == videoId);
            return Task.FromResult(video);
        }

        public Task UpdatePlaybackSessionAsync(Guid userId, Guid videoId, int stoppedAtSeconds)
        {
            var session = _mockSessions.FirstOrDefault(s => s.UserId == userId && s.VideoId == videoId);
            if (session == null)
            {
                session = new StreamingSession { Id = Guid.NewGuid(), UserId = userId, VideoId = videoId };
                _mockSessions.Add(session);
            }
            
            session.StoppedAtSeconds = stoppedAtSeconds;
            session.LastWatchedAt = DateTime.UtcNow;

            return Task.CompletedTask;
        }

        public Task<int> GetResumePositionAsync(Guid userId, Guid videoId)
        {
            var session = _mockSessions.FirstOrDefault(s => s.UserId == userId && s.VideoId == videoId);
            return Task.FromResult(session?.StoppedAtSeconds ?? 0);
        }
    }
}
