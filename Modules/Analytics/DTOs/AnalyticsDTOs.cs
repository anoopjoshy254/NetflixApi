using System.Collections.Generic;

namespace NetflixApi.Modules.Analytics.DTOs
{
    public class TrackEventRequestDto
    {
        public int? ContentId { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
    }

    public class PopularContentDto
    {
        public int ContentId { get; set; }
        public int WatchCount { get; set; }
    }

    public class WatchStatsResponseDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UserWatchStatDto> Stats { get; set; }
    }

    public class UserWatchStatDto
    {
        public int UserId { get; set; }
        public int TotalWatchEvents { get; set; }
    }

    public class RevenueTrendDto
    {
        public string Month { get; set; }
        public decimal Revenue { get; set; }
    }
}
