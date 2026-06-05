using System;
using System.Collections.Generic;

namespace Netflix.API.Modules.Content.DTOs.Movie
{
    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public string MaturityRating { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}
