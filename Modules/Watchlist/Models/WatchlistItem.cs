namespace NetflixApi.Modules.Watchlist.Models
{
    public class WatchlistItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
