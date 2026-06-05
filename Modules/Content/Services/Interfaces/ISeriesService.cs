using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Series;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<SeriesResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<SeriesResponseDto>> GetAllAsync();
        Task<SeriesResponseDto> CreateAsync(CreateSeriesDto dto);
        Task UpdateAsync(int id, UpdateSeriesDto dto);
        Task DeleteAsync(int id);
    }
}
