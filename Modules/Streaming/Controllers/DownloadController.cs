using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Streaming.Services;
using NetflixApi.Modules.Streaming.DTOs;

namespace NetflixApi.Modules.Streaming.Controllers
{
    [ApiController]
    [Route("api/downloads")]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadService _downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }

        [HttpPost]
        public async Task<IActionResult> DownloadVideo([FromBody] DownloadRequestDto dto)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var result = await _downloadService.ProcessDownloadAsync(userId, dto.VideoId, dto.Quality);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDownloads()
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var downloads = await _downloadService.GetUserDownloadsAsync(userId);
            return Ok(downloads);
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> RemoveDownload(Guid videoId)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _downloadService.RemoveDownloadAsync(userId, videoId);
            return Ok();
        }
    }
}
