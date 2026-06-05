using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Streaming.Services;
using NetflixApi.Modules.Streaming.DTOs;

namespace NetflixApi.Modules.Streaming.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToHistory([FromBody] WatchHistoryDto dto)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _historyService.AddToHistoryAsync(userId, dto.VideoId, dto.Completed);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var history = await _historyService.GetUserHistoryAsync(userId);
            return Ok(history);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearHistory()
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _historyService.ClearHistoryAsync(userId);
            return Ok();
        }
    }
}
