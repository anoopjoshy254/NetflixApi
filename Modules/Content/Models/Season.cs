using System;
using System.Collections.Generic;

namespace Netflix.API.Modules.Content.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public Series Series { get; set; }
        public int SeasonNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
