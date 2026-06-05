namespace Netflix.API.Modules.Content.DTOs.Episode
{
    public class EpisodeResponseDto
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public string VideoUrl { get; set; } = string.Empty;
    }
}
