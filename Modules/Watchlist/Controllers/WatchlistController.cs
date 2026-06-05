using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Watchlist.Services;
using NetflixApi.Modules.Watchlist.DTOs;

namespace NetflixApi.Modules.Watchlist.Controllers
{
    [ApiController]
    [Route("api/watchlist")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist([FromBody] AddToWatchlistDto dto)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var result = await _watchlistService.AddToWatchlistAsync(userId, dto.VideoId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWatchlist()
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var watchlist = await _watchlistService.GetUserWatchlistAsync(userId);
            return Ok(watchlist);
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> RemoveFromWatchlist(Guid videoId)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _watchlistService.RemoveFromWatchlistAsync(userId, videoId);
            return Ok();
        }
    }
}
