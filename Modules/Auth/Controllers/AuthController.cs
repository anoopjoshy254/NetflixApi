using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Auth.DTOs;
using NetflixApi.Modules.Auth.Services;

namespace NetflixApi.Modules.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.RegisterAsync(dto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "User registered successfully. Please verify your email."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.RegisterAdminAsync(dto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Admin registered successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                var response = await _authService.LoginAsync(dto);
                return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(response, "Login successful."));
            }
            catch (Exception ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                var response = await _authService.RefreshTokenAsync(dto);
                return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(response, "Token refreshed successfully."));
            }
            catch (Exception ex)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.LogoutAsync(dto.RefreshToken);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Logout successful."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.ForgotPasswordAsync(dto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "If the email is registered, a password reset link has been generated."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.ResetPasswordAsync(dto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Password reset successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));
                }

                await _authService.VerifyEmailAsync(dto.Token);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Email verified successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
