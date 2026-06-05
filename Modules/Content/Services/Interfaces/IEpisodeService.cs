using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.DTOs.Episode;

namespace Netflix.API.Modules.Content.Services.Interfaces
{
    public interface IEpisodeService
    {
        Task<EpisodeResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<EpisodeResponseDto>> GetBySeasonIdAsync(int seasonId);
        Task<EpisodeResponseDto> CreateAsync(CreateEpisodeDto dto);
        Task UpdateAsync(int id, UpdateEpisodeDto dto);
        Task DeleteAsync(int id);
    }
}
