using System;

namespace Netflix.API.Modules.Content.DTOs.Season
{
    public class SeasonResponseDto
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public int SeasonNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
    }
}
