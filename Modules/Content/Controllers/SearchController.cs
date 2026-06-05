using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Modules.Content.Services.Interfaces;

namespace Netflix.API.Modules.Content.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return BadRequest("Query cannot be empty.");
            
            var results = await _searchService.SearchAsync(query);
            return Ok(results);
        }
    }
}
