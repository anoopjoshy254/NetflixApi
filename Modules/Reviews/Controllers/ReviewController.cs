using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Reviews.Services;
using NetflixApi.Modules.Reviews.DTOs;

namespace NetflixApi.Modules.Reviews.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDto dto)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var result = await _reviewService.AddReviewAsync(userId, dto.VideoId, dto.Text);
            return Ok(result);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> GetReviews(Guid videoId)
        {
            var reviews = await _reviewService.GetVideoReviewsAsync(videoId);
            return Ok(reviews);
        }

        [HttpPost("rating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingDto dto)
        {
            var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var result = await _reviewService.AddRatingAsync(userId, dto.VideoId, dto.IsLike);
            return Ok(result);
        }

        [HttpGet("rating/{videoId}")]
        public async Task<IActionResult> GetRatings(Guid videoId)
        {
            var (likes, dislikes) = await _reviewService.GetVideoRatingsAsync(videoId);
            return Ok(new { Likes = likes, Dislikes = dislikes });
        }
    }
}
