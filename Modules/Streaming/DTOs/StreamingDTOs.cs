namespace NetflixApi.Modules.Streaming.DTOs
{
    public class PlaybackSessionDto
    {
        public Guid VideoId { get; set; }
        public int StoppedAtSeconds { get; set; }
    }

    public class WatchHistoryDto
    {
        public Guid VideoId { get; set; }
        public DateTime WatchedAt { get; set; }
        public bool Completed { get; set; }
    }

    public class DownloadRequestDto
    {
        public Guid VideoId { get; set; }
        public string Quality { get; set; } = "720p";
    }
}
