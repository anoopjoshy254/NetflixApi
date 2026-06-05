using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;

namespace Netflix.API.Modules.Content.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly ApplicationDbContext _context;

        public SeasonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Season> GetByIdAsync(int id)
        {
            return await _context.Seasons.FindAsync(id);
        }

        public async Task<IEnumerable<Season>> GetBySeriesIdAsync(int seriesId)
        {
            return await _context.Seasons
                .Where(s => s.SeriesId == seriesId)
                .OrderBy(s => s.SeasonNumber)
                .ToListAsync();
        }

        public async Task<Season> AddAsync(Season season)
        {
            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();
            return season;
        }
    }
}
