using System;

namespace NetflixApi.Modules.Users.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public bool IsKidsProfile { get; set; }
    }
}
