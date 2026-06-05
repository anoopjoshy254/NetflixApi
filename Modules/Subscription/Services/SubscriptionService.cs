using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetflixApi.Modules.Subscription.DTOs;
using NetflixApi.Modules.Subscription.Interfaces;
// Assumed namespaces for AppDbContext and Models based on the team's structure
using NetflixApi.Data;
using NetflixApi.Models;

namespace NetflixApi.Modules.Subscription.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(AppDbContext context, ILogger<SubscriptionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<SubscriptionPlanDto>> GetActivePlansAsync()
        {
            var plans = await _context.SubscriptionPlans
                .Where(p => p.IsActive)
                .ToListAsync();

            return plans.Select(p => new SubscriptionPlanDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                MaxProfiles = p.MaxProfiles,
                VideoQuality = p.VideoQuality
            });
        }

        public async Task<bool> SubscribeAsync(int userId, int planId)
        {
            var activeSub = await _context.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (activeSub != null) return false;

            var plan = await _context.SubscriptionPlans.FindAsync(planId);
            if (plan == null || !plan.IsActive) return false;

            var newSub = new UserSubscription
            {
                UserId = userId,
                PlanId = planId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                Status = "Active",
                AutoRenew = true
            };

            _context.UserSubscriptions.Add(newSub);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpgradeAsync(int userId, int newPlanId)
        {
            return await ChangePlanAsync(userId, newPlanId, isUpgrade: true);
        }

        public async Task<bool> DowngradeAsync(int userId, int newPlanId)
        {
            return await ChangePlanAsync(userId, newPlanId, isUpgrade: false);
        }

        private async Task<bool> ChangePlanAsync(int userId, int newPlanId, bool isUpgrade)
        {
            var activeSub = await _context.UserSubscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (activeSub == null) return false;

            var newPlan = await _context.SubscriptionPlans.FindAsync(newPlanId);
            if (newPlan == null || !newPlan.IsActive) return false;

            // Simplified logic: If upgrade, new plan price should be higher
            if (isUpgrade && newPlan.Price <= activeSub.Plan.Price) return false;
            if (!isUpgrade && newPlan.Price >= activeSub.Plan.Price) return false;

            activeSub.PlanId = newPlanId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelAsync(int userId)
        {
            var activeSub = await _context.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (activeSub == null) return false;

            activeSub.AutoRenew = false;
            activeSub.Status = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserSubscriptionDto> GetMySubscriptionAsync(int userId)
        {
            var sub = await _context.UserSubscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (sub == null) return null;

            return new UserSubscriptionDto
            {
                Id = sub.Id,
                PlanId = sub.PlanId,
                PlanName = sub.Plan?.Name,
                StartDate = sub.StartDate,
                EndDate = sub.EndDate,
                Status = sub.Status,
                AutoRenew = sub.AutoRenew
            };
        }

        public async Task<SubscriptionStatusDto> GetStatusAsync(int userId)
        {
            var sub = await _context.UserSubscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");

            if (sub == null) return new SubscriptionStatusDto { IsActive = false };

            return new SubscriptionStatusDto
            {
                IsActive = true,
                PlanName = sub.Plan?.Name
            };
        }

        public async Task<bool> IsActiveAsync(int userId)
        {
            var sub = await _context.UserSubscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == "Active");
            return sub != null;
        }
    }
}
