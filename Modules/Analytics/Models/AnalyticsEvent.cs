using System;

namespace NetflixApi.Modules.Analytics.Models
{
    public class AnalyticsEvent
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? ContentId { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}
