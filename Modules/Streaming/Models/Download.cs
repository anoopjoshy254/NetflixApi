namespace NetflixApi.Modules.Streaming.Models
{
    public class Download
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime DownloadedAt { get; set; }
        public DateTime ExpiresAt { get; set; } // E.g., downloads expire after 30 days
        public string Quality { get; set; } = "720p";
    }
}
