namespace NetflixApi.Modules.Reviews.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
