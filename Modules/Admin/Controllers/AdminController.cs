using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixApi.Modules.Admin.DTOs;
using NetflixApi.Data;

namespace NetflixApi.Modules.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;

            var totalUsers = await _context.Users.CountAsync();
            var activeSubscriptions = await _context.UserSubscriptions.CountAsync(s => s.Status == "Active");
            
            // Note: Assuming Content table is named Contents or Content in DbContext
            var totalContent = await _context.Contents.CountAsync(); 
            
            var totalRevenue = await _context.Payments
                .Where(p => p.Status == "Success" && p.CreatedAt.Month == currentMonth && p.CreatedAt.Year == currentYear)
                .SumAsync(p => p.Amount);

            return Ok(new DashboardStatsDto
            {
                TotalUsers = totalUsers,
                ActiveSubscriptions = activeSubscriptions,
                TotalRevenueThisMonth = totalRevenue,
                TotalContentCount = totalContent
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Users.AsQueryable();
            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email, // Assuming User has Email
                    IsActive = u.IsActive, // Assuming User has IsActive
                    SubscriptionStatus = _context.UserSubscriptions
                        .Where(s => s.UserId == u.Id && s.Status == "Active")
                        .Select(s => s.Status)
                        .FirstOrDefault() ?? "None"
                })
                .ToListAsync();

            return Ok(new UserListResponseDto
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Users = users
            });
        }

        [HttpPut("users/{id}/ban")]
        public async Task<IActionResult> BanUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = false; // Assuming IsActive exists
            await _context.SaveChangesAsync();
            return Ok(new { message = "User banned successfully" });
        }

        [HttpPut("users/{id}/unban")]
        public async Task<IActionResult> UnbanUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = true;
            await _context.SaveChangesAsync();
            return Ok(new { message = "User unbanned successfully" });
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var subs = await _context.UserSubscriptions
                .Include(s => s.User)
                .Include(s => s.Plan)
                .Select(s => new AdminSubscriptionDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    UserEmail = s.User.Email, // Assuming User has Email
                    PlanName = s.Plan.Name,
                    Status = s.Status
                })
                .ToListAsync();

            return Ok(subs);
        }
    }
}
