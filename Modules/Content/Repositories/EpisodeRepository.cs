using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Data;
using Netflix.API.Modules.Content.Models;
using Netflix.API.Modules.Content.Repositories.Interfaces;

namespace Netflix.API.Modules.Content.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly ApplicationDbContext _context;

        public EpisodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Episode> GetByIdAsync(int id)
        {
            return await _context.Episodes.FindAsync(id);
        }

        public async Task<IEnumerable<Episode>> GetBySeasonIdAsync(int seasonId)
        {
            return await _context.Episodes
                .Where(e => e.SeasonId == seasonId)
                .OrderBy(e => e.EpisodeNumber)
                .ToListAsync();
        }

        public async Task<Episode> AddAsync(Episode episode)
        {
            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();
            return episode;
        }

        public async Task UpdateAsync(Episode episode)
        {
            _context.Episodes.Update(episode);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            if (episode != null)
            {
                _context.Episodes.Remove(episode);
                await _context.SaveChangesAsync();
            }
        }
    }
}
