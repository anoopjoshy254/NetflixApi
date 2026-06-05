namespace Netflix.API.Modules.Content.DTOs.Series
{
    public class SeriesResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string MaturityRating { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
