using System.Collections.Generic;
using Netflix.API.Modules.Content.DTOs.Movie;
using Netflix.API.Modules.Content.DTOs.Series;

namespace Netflix.API.Modules.Content.DTOs.Search
{
    public class SearchResponseDto
    {
        public List<MovieResponseDto> Movies { get; set; } = new List<MovieResponseDto>();
        public List<SeriesResponseDto> Series { get; set; } = new List<SeriesResponseDto>();
    }
}
