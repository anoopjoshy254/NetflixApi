using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetflixApi.Modules.Reports.DTOs;
using NetflixApi.Modules.Reports.Interfaces;
using NetflixApi.Data;

namespace NetflixApi.Modules.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReportService> _logger;

        public ReportService(AppDbContext context, ILogger<ReportService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<RevenueReportDto>> GetRevenueReportAsync(DateTime from, DateTime to)
        {
            // Note: Invoices/Payments don't have Plan directly, but we can join via UserSubscriptions
            // For simplicity, we assume we just find the revenue per plan using the user's active subscription during that time.
            var data = await _context.UserSubscriptions
                .Include(s => s.Plan)
                .Where(s => s.StartDate >= from && s.StartDate <= to && s.Status != "Cancelled")
                .GroupBy(s => s.Plan.Name)
                .Select(g => new RevenueReportDto
                {
                    PlanName = g.Key,
                    TotalRevenue = g.Sum(s => s.Plan.Price),
                    SubscriptionCount = g.Count()
                })
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<SubscriptionStatusReportDto>> GetSubscriptionsReportAsync()
        {
            var data = await _context.UserSubscriptions
                .Include(s => s.Plan)
                .GroupBy(s => s.Plan.Name)
                .Select(g => new SubscriptionStatusReportDto
                {
                    PlanName = g.Key,
                    Active = g.Count(s => s.Status == "Active"),
                    Cancelled = g.Count(s => s.Status == "Cancelled"),
                    Expired = g.Count(s => s.Status == "Expired")
                })
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<UserRegistrationReportDto>> GetUsersReportAsync(int days)
        {
            var cutoff = DateTime.UtcNow.AddDays(-days);

            // Assuming User model has CreatedAt
            var data = await _context.Users
                .Where(u => u.CreatedAt >= cutoff)
                .GroupBy(u => u.CreatedAt.Date)
                .Select(g => new UserRegistrationReportDto
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    NewUsersCount = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<ViewingReportDto>> GetViewingReportAsync()
        {
            var data = await _context.Analytics
                .Where(a => a.EventType == "Watch" && a.ContentId != null)
                .GroupBy(a => a.ContentId.Value)
                .Select(g => new ViewingReportDto
                {
                    ContentId = g.Key,
                    WatchCount = g.Count()
                })
                .OrderByDescending(v => v.WatchCount)
                .Take(10)
                .ToListAsync();

            return data;
        }
    }
}
