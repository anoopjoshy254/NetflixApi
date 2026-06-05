namespace NetflixApi.Modules.Reviews.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public bool IsLike { get; set; } // true for like, false for dislike
        public DateTime CreatedAt { get; set; }
    }
}
