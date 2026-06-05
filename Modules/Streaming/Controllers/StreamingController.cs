using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Streaming.Services;
using NetflixApi.Modules.Streaming.DTOs;

namespace NetflixApi.Modules.Streaming.Controllers
{
    [ApiController]
    [Route("api/streaming")]
    public class StreamingController : ControllerBase
    {
        private readonly IStreamingService _streamingService;
        private readonly IWebHostEnvironment _env;

        public StreamingController(IStreamingService streamingService, IWebHostEnvironment env)
        {
            _streamingService = streamingService;
            _env = env;
        }

        [HttpGet("play/{videoId}")]
        public async Task<IActionResult> PlayVideo(Guid videoId)
        {
            var video = await _streamingService.GetVideoAsync(videoId);
            
            // To simulate "free" netflix-like byte range streaming:
            // In a real app we'd map this to a physical file or remote blob storage
            // Here, we'll try to find a sample file in wwwroot or return a dummy response
            var path = Path.Combine(_env.WebRootPath ?? _env.ContentRootPath, "wwwroot", video?.Url ?? "sample.mp4");
            
            if (!System.IO.File.Exists(path))
            {
                // Create a dummy 1MB file to demonstrate streaming if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                using (var fs = new FileStream(path, FileMode.CreateNew))
                {
                    fs.SetLength(1024 * 1024); // 1MB dummy file
                }
            }

            return PhysicalFile(path, "video/mp4", enableRangeProcessing: true);
        }

        [HttpPost("session")]
        public async Task<IActionResult> UpdateSession([FromBody] PlaybackSessionDto dto)
        {
            // Hardcode userId for now as there's no auth
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            await _streamingService.UpdatePlaybackSessionAsync(userId, dto.VideoId, dto.StoppedAtSeconds);
            return Ok();
        }

        [HttpGet("session/{videoId}")]
        public async Task<IActionResult> GetResumePosition(Guid videoId)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var position = await _streamingService.GetResumePositionAsync(userId, videoId);
            return Ok(new { ResumePositionSeconds = position });
        }
    }
}
