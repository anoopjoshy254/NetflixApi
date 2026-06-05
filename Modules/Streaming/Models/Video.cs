namespace NetflixApi.Modules.Streaming.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // Local path or CDN url
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int DurationInSeconds { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
