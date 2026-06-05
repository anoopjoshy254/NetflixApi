using System;

namespace NetflixApi.Modules.Users.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public bool IsKidsProfile { get; set; }
    }
}
