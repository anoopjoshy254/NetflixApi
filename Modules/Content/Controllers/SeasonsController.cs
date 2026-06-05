using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.API.Modules.Content.DTOs.Season;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonsController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var season = await _seasonService.GetByIdAsync(id);
            if (season == null) return NotFound();
            return Ok(season);
        }

        [HttpGet("series/{seriesId}")]
        public async Task<IActionResult> GetBySeriesId(int seriesId)
        {
            var seasons = await _seasonService.GetBySeriesIdAsync(seriesId);
            return Ok(seasons);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSeasonDto dto)
        {
            var created = await _seasonService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
    }
}
