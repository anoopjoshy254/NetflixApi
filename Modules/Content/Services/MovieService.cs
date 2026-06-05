using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Genre;
using Netflix.API.Modules.Content.DTOs.Movie;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        private MovieResponseDto MapToDto(Movie movie)
        {
            if (movie == null) return null;
            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                DurationMinutes = movie.DurationMinutes,
                MaturityRating = movie.MaturityRating,
                PosterUrl = movie.PosterUrl,
                VideoUrl = movie.VideoUrl,
                IsActive = movie.IsActive,
                Genres = movie.MovieGenres?.Select(mg => new GenreResponseDto 
                { 
                    Id = mg.Genre.Id, 
                    Name = mg.Genre.Name 
                }).ToList() ?? new List<GenreResponseDto>()
            };
        }

        public async Task<MovieResponseDto> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return MapToDto(movie);
        }

        public async Task<IEnumerable<MovieResponseDto>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies.Select(MapToDto);
        }

        public async Task<MovieResponseDto> CreateAsync(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                DurationMinutes = dto.DurationMinutes,
                MaturityRating = dto.MaturityRating,
                PosterUrl = dto.PosterUrl,
                VideoUrl = dto.VideoUrl,
                IsActive = true
            };

            foreach (var genreId in dto.GenreIds)
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                if (genre == null)
                {
                    throw new Exception($"Genre with ID {genreId} does not exist.");
                }
                movie.MovieGenres.Add(new MovieGenre { GenreId = genreId });
            }

            var created = await _movieRepository.AddAsync(movie);
            return MapToDto(created);
        }

        public async Task UpdateAsync(int id, UpdateMovieDto dto)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                movie.Title = dto.Title;
                movie.Description = dto.Description;
                movie.ReleaseDate = dto.ReleaseDate;
                movie.DurationMinutes = dto.DurationMinutes;
                movie.MaturityRating = dto.MaturityRating;
                movie.PosterUrl = dto.PosterUrl;
                movie.VideoUrl = dto.VideoUrl;
                movie.IsActive = dto.IsActive;

                // Update genres logic can be complex (add/remove), skipping for brevity
                await _movieRepository.UpdateAsync(movie);
            }
            else
            {
                throw new Exception($"Movie with ID {id} does not exist.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _movieRepository.DeleteAsync(id);
        }
    }
}
