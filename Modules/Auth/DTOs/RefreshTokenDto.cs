using System.ComponentModel.DataAnnotations;

namespace NetflixApi.Modules.Auth.DTOs
{
    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
