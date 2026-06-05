using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Genre;
using Netflix.API.Modules.Content.DTOs.Movie;
using Netflix.API.Modules.Content.DTOs.Search;
using Netflix.API.Modules.Content.DTOs.Series;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class SearchService : ISearchService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISeriesRepository _seriesRepository;

        public SearchService(IMovieRepository movieRepository, ISeriesRepository seriesRepository)
        {
            _movieRepository = movieRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<SearchResponseDto> SearchAsync(string query)
        {
            var movies = await _movieRepository.SearchAsync(query);
            var series = await _seriesRepository.SearchAsync(query);

            return new SearchResponseDto
            {
                Movies = movies.Select(m => new MovieResponseDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ReleaseDate = m.ReleaseDate,
                    DurationMinutes = m.DurationMinutes,
                    MaturityRating = m.MaturityRating,
                    PosterUrl = m.PosterUrl,
                    VideoUrl = m.VideoUrl,
                    IsActive = m.IsActive,
                    Genres = m.MovieGenres?.Select(mg => new GenreResponseDto { Id = mg.Genre.Id, Name = mg.Genre.Name }).ToList() ?? new System.Collections.Generic.List<GenreResponseDto>()
                }).ToList(),
                Series = series.Select(s => new SeriesResponseDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    ReleaseYear = s.ReleaseYear,
                    MaturityRating = s.MaturityRating,
                    PosterUrl = s.PosterUrl,
                    IsActive = s.IsActive
                }).ToList()
            };
        }
    }
}
