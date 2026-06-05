using System.ComponentModel.DataAnnotations;

namespace NetflixApi.Modules.Users.DTOs
{
    public class CreateProfileDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        
        public string? AvatarUrl { get; set; }
        
        public bool IsKidsProfile { get; set; }
    }
}
