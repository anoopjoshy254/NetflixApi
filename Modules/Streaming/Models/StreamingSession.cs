namespace NetflixApi.Modules.Streaming.Models
{
    public class StreamingSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public int StoppedAtSeconds { get; set; }
        public DateTime LastWatchedAt { get; set; }
    }
}
