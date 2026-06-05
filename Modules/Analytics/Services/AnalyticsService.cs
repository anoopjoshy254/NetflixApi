using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetflixApi.Modules.Analytics.DTOs;
using NetflixApi.Modules.Analytics.Interfaces;
using NetflixApi.Data;
using NetflixApi.Models;

namespace NetflixApi.Modules.Analytics.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(AppDbContext context, ILogger<AnalyticsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> TrackEventAsync(int? userId, TrackEventRequestDto request)
        {
            var analyticsEvent = new NetflixApi.Models.Analytics
            {
                UserId = userId,
                ContentId = request.ContentId,
                EventType = request.EventType,
                EventData = request.EventData,
                OccurredAt = DateTime.UtcNow
            };

            _context.Analytics.Add(analyticsEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PopularContentDto>> GetPopularContentAsync()
        {
            var data = await _context.Analytics
                .Where(a => a.EventType == "Watch" && a.ContentId != null)
                .GroupBy(a => a.ContentId.Value)
                .Select(g => new PopularContentDto
                {
                    ContentId = g.Key,
                    WatchCount = g.Count()
                })
                .OrderByDescending(v => v.WatchCount)
                .Take(10)
                .ToListAsync();

            return data;
        }

        public async Task<WatchStatsResponseDto> GetWatchStatsAsync(int page, int pageSize)
        {
            var query = _context.Analytics
                .Where(a => a.EventType == "Watch" && a.UserId != null)
                .GroupBy(a => a.UserId.Value);

            var totalCount = await query.CountAsync();

            var stats = await query
                .Select(g => new UserWatchStatDto
                {
                    UserId = g.Key,
                    TotalWatchEvents = g.Count()
                })
                .OrderByDescending(s => s.TotalWatchEvents)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new WatchStatsResponseDto
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Stats = stats
            };
        }

        public async Task<IEnumerable<RevenueTrendDto>> GetRevenueTrendAsync()
        {
            var cutoff = DateTime.UtcNow.AddMonths(-12);
            var data = await _context.Payments
                .Where(p => p.Status == "Success" && p.CreatedAt >= cutoff)
                .GroupBy(p => new { p.CreatedAt.Year, p.CreatedAt.Month })
                .Select(g => new RevenueTrendDto
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Revenue = g.Sum(p => p.Amount)
                })
                .OrderBy(d => d.Month)
                .ToListAsync();

            return data;
        }
    }
}
