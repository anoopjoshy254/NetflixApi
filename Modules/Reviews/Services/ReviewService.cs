using NetflixApi.Modules.Reviews.Models;

namespace NetflixApi.Modules.Reviews.Services
{
    public class ReviewService : IReviewService
    {
        private static readonly List<Review> _mockReviews = new List<Review>();
        private static readonly List<Rating> _mockRatings = new List<Rating>();

        public Task<Review> AddReviewAsync(Guid userId, Guid videoId, string text)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                VideoId = videoId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };
            _mockReviews.Add(review);
            return Task.FromResult(review);
        }

        public Task<IEnumerable<Review>> GetVideoReviewsAsync(Guid videoId)
        {
            var reviews = _mockReviews.Where(r => r.VideoId == videoId).OrderByDescending(r => r.CreatedAt);
            return Task.FromResult<IEnumerable<Review>>(reviews);
        }

        public Task<Rating> AddRatingAsync(Guid userId, Guid videoId, bool isLike)
        {
            var existing = _mockRatings.FirstOrDefault(r => r.UserId == userId && r.VideoId == videoId);
            if (existing != null)
            {
                existing.IsLike = isLike;
                existing.CreatedAt = DateTime.UtcNow;
                return Task.FromResult(existing);
            }

            var rating = new Rating
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                VideoId = videoId,
                IsLike = isLike,
                CreatedAt = DateTime.UtcNow
            };
            _mockRatings.Add(rating);
            return Task.FromResult(rating);
        }

        public Task<(int Likes, int Dislikes)> GetVideoRatingsAsync(Guid videoId)
        {
            var likes = _mockRatings.Count(r => r.VideoId == videoId && r.IsLike);
            var dislikes = _mockRatings.Count(r => r.VideoId == videoId && !r.IsLike);
            return Task.FromResult((likes, dislikes));
        }
    }
}
