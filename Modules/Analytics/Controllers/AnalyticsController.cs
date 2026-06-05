using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Analytics.DTOs;
using NetflixApi.Modules.Analytics.Interfaces;

namespace NetflixApi.Modules.Analytics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpPost("track")]
        public async Task<IActionResult> TrackEvent([FromBody] TrackEventRequestDto request)
        {
            Guid? userId = null;
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userIdClaim, out var parsedId))
            {
                userId = parsedId;
            }

            await _analyticsService.TrackEventAsync(userId, request);
            return Ok();
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularContent()
        {
            var content = await _analyticsService.GetPopularContentAsync();
            return Ok(content);
        }

        [HttpGet("watch-stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWatchStats([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var stats = await _analyticsService.GetWatchStatsAsync(page, pageSize);
            return Ok(stats);
        }

        [HttpGet("revenue-trend")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRevenueTrend()
        {
            var trend = await _analyticsService.GetRevenueTrendAsync();
            return Ok(trend);
        }
    }
}
