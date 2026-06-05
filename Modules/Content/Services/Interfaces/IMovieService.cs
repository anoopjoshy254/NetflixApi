using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Movie;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface IMovieService
    {
        Task<MovieResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<MovieResponseDto>> GetAllAsync();
        Task<MovieResponseDto> CreateAsync(CreateMovieDto dto);
        Task UpdateAsync(int id, UpdateMovieDto dto);
        Task DeleteAsync(int id);
    }
}
