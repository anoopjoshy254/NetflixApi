using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Genre;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<GenreResponseDto>> GetAllAsync();
        Task<GenreResponseDto> CreateAsync(CreateGenreDto dto);
    }
}
