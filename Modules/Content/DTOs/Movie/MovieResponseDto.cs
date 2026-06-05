using System;
using System.Collections.Generic;
using Netflix.API.Modules.Content.DTOs.Genre;

namespace Netflix.API.Modules.Content.DTOs.Movie
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public string MaturityRating { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<GenreResponseDto> Genres { get; set; } = new List<GenreResponseDto>();
    }
}
