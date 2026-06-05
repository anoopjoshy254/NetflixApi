using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixApi.Modules.Users.DTOs;
using NetflixApi.Modules.Users.Services;
using NetflixApi.Modules.Auth.DTOs;

namespace NetflixApi.Modules.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private Guid GetUserId()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdStr, out var userId)) return userId;
            throw new Exception("Unauthorized.");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userId = GetUserId();
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(ApiResponse<UserDto>.SuccessResponse(user, "User fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));

                var userId = GetUserId();
                var user = await _userService.UpdateUserAsync(userId, dto);
                return Ok(ApiResponse<UserDto>.SuccessResponse(user, "User updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("me")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            try
            {
                var userId = GetUserId();
                await _userService.SoftDeleteUserAsync(userId);
                return Ok(ApiResponse<object>.SuccessResponse(null, "User deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
