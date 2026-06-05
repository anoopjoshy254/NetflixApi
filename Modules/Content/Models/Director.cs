using System.Collections.Generic;

namespace Netflix.API.Modules.Content.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
