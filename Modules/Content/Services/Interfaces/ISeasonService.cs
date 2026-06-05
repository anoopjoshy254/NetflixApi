using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Season;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface ISeasonService
    {
        Task<SeasonResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<SeasonResponseDto>> GetBySeriesIdAsync(int seriesId);
        Task<SeasonResponseDto> CreateAsync(CreateSeasonDto dto);
    }
}
