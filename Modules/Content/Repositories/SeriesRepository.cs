using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;

namespace Netflix.API.Modules.Content.Repositories
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly ApplicationDbContext _context;

        public SeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Series> GetByIdAsync(int id)
        {
            return await _context.Series
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Series>> GetAllAsync()
        {
            return await _context.Series.ToListAsync();
        }

        public async Task<Series> AddAsync(Series series)
        {
            _context.Series.Add(series);
            await _context.SaveChangesAsync();
            return series;
        }

        public async Task UpdateAsync(Series series)
        {
            _context.Series.Update(series);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series != null)
            {
                _context.Series.Remove(series);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Series>> SearchAsync(string query)
        {
            return await _context.Series
                .Where(s => s.Title.Contains(query) || s.Description.Contains(query))
                .ToListAsync();
        }
    }
}
