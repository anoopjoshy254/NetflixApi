using System.Collections.Generic;

namespace Netflix.API.Modules.Content.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
