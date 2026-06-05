using System.Collections.Generic;
using System.Threading.Tasks;
using Netflix.API.Modules.Content.Models;

namespace Netflix.API.Modules.Content.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
        Task<IEnumerable<Movie>> SearchAsync(string query);
    }
}
