using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.Models;

namespace Netflix.API.Modules.Content.Repositories.Interfaces
{
    public interface IEpisodeRepository
    {
        Task<Episode> GetByIdAsync(int id);
        Task<IEnumerable<Episode>> GetBySeasonIdAsync(int seasonId);
        Task<Episode> AddAsync(Episode episode);
        Task UpdateAsync(Episode episode);
        Task DeleteAsync(int id);
    }
}
