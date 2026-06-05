using System;

namespace NetflixApi.Modules.Notifications.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
    }
}
