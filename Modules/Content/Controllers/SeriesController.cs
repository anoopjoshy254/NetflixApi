using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Netflix.API.Modules.Content.DTOs.Series;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var series = await _seriesService.GetAllAsync();
            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var series = await _seriesService.GetByIdAsync(id);
            if (series == null) return NotFound();
            return Ok(series);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSeriesDto dto)
        {
            var created = await _seriesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSeriesDto dto)
        {
            await _seriesService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _seriesService.DeleteAsync(id);
            return NoContent();
        }
    }
}
