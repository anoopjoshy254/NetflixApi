using System.Threading.Tasks;
using NetflixApi.Modules.Auth.DTOs;

namespace NetflixApi.Modules.Auth.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto dto);
        Task RegisterAdminAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task LogoutAsync(string refreshToken);
        Task ForgotPasswordAsync(ForgotPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task VerifyEmailAsync(string token);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
    }
}
