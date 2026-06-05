using System;
using System.Collections.Generic;

namespace Netflix.API.Modules.Content.DTOs.Movie
{
    public class UpdateMovieDto : CreateMovieDto
    {
        public bool IsActive { get; set; }
    }
}
