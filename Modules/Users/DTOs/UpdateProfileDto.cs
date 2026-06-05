using System.ComponentModel.DataAnnotations;

namespace NetflixApi.Modules.Users.DTOs
{
    public class UpdateProfileDto
    {
        [MaxLength(50)]
        public string? Name { get; set; }
        
        public string? AvatarUrl { get; set; }
        
        public bool? IsKidsProfile { get; set; }
    }
}
