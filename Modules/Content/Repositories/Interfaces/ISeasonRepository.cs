using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.Models;

namespace Netflix.API.Modules.Content.Repositories.Interfaces
{
    public interface ISeasonRepository
    {
        Task<Season> GetByIdAsync(int id);
        Task<IEnumerable<Season>> GetBySeriesIdAsync(int seriesId);
        Task<Season> AddAsync(Season season);
    }
}
