using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.Models;

namespace Netflix.API.Modules.Content.Repositories.Interfaces
{
    public interface ISeriesRepository
    {
        Task<Series> GetByIdAsync(int id);
        Task<IEnumerable<Series>> GetAllAsync();
        Task<Series> AddAsync(Series series);
        Task UpdateAsync(Series series);
        Task DeleteAsync(int id);
        Task<IEnumerable<Series>> SearchAsync(string query);
    }
}
