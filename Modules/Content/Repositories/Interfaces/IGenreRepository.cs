using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.Models;

namespace Netflix.API.Modules.Content.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> AddAsync(Genre genre);
    }
}
