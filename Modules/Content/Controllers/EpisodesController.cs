using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.API.Modules.Content.DTOs.Episode;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;

        public EpisodesController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var episode = await _episodeService.GetByIdAsync(id);
            if (episode == null) return NotFound();
            return Ok(episode);
        }

        [HttpGet("season/{seasonId}")]
        public async Task<IActionResult> GetBySeasonId(int seasonId)
        {
            var episodes = await _episodeService.GetBySeasonIdAsync(seasonId);
            return Ok(episodes);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEpisodeDto dto)
        {
            var created = await _episodeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEpisodeDto dto)
        {
            await _episodeService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _episodeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
