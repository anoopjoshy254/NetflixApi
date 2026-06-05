namespace NetflixApi.Modules.Reviews.DTOs
{
    public class AddReviewDto
    {
        public Guid VideoId { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class AddRatingDto
    {
        public Guid VideoId { get; set; }
        public bool IsLike { get; set; }
    }
}
