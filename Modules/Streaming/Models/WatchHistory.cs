namespace NetflixApi.Modules.Streaming.Models
{
    public class WatchHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime WatchedAt { get; set; }
        public bool Completed { get; set; }
    }
}
