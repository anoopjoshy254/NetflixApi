using System;
using System.Collections.Generic;

namespace Netflix.API.Modules.Content.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string MaturityRating { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Season> Seasons { get; set; } = new List<Season>();
    }
}
