using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Reports.Interfaces;

namespace NetflixApi.Modules.Reports.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenue([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            if (from == default || to == default)
                return BadRequest("Valid from and to dates are required.");

            var report = await _reportService.GetRevenueReportAsync(from, to);
            return Ok(report);
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var report = await _reportService.GetSubscriptionsReportAsync();
            return Ok(report);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int days = 30)
        {
            var report = await _reportService.GetUsersReportAsync(days);
            return Ok(report);
        }

        [HttpGet("viewing")]
        public async Task<IActionResult> GetViewing()
        {
            var report = await _reportService.GetViewingReportAsync();
            return Ok(report);
        }
    }
}
