using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Genre;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponseDto> GetByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) return null;

            return new GenreResponseDto { Id = genre.Id, Name = genre.Name };
        }

        public async Task<IEnumerable<GenreResponseDto>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(g => new GenreResponseDto { Id = g.Id, Name = g.Name });
        }

        public async Task<GenreResponseDto> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            var created = await _genreRepository.AddAsync(genre);
            return new GenreResponseDto { Id = created.Id, Name = created.Name };
        }
    }
}
