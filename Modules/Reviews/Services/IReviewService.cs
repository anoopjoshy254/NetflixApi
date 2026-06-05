using NetflixApi.Modules.Reviews.Models;

namespace NetflixApi.Modules.Reviews.Services
{
    public interface IReviewService
    {
        Task<Review> AddReviewAsync(Guid userId, Guid videoId, string text);
        Task<IEnumerable<Review>> GetVideoReviewsAsync(Guid videoId);
        
        Task<Rating> AddRatingAsync(Guid userId, Guid videoId, bool isLike);
        Task<(int Likes, int Dislikes)> GetVideoRatingsAsync(Guid videoId);
    }
}
