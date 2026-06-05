using System;
using System.Collections.Generic;
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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        private Guid GetUserId()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdStr, out var userId)) return userId;
            throw new Exception("Unauthorized.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            try
            {
                var userId = GetUserId();
                var profiles = await _profileService.GetProfilesAsync(userId);
                return Ok(ApiResponse<IEnumerable<ProfileDto>>.SuccessResponse(profiles, "Profiles fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));

                var userId = GetUserId();
                var profile = await _profileService.CreateProfileAsync(userId, dto);
                return Ok(ApiResponse<ProfileDto>.SuccessResponse(profile, "Profile created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.ErrorResponse("Invalid payload", ModelState));

                var userId = GetUserId();
                var profile = await _profileService.UpdateProfileAsync(userId, id, dto);
                return Ok(ApiResponse<ProfileDto>.SuccessResponse(profile, "Profile updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            try
            {
                var userId = GetUserId();
                await _profileService.DeleteProfileAsync(userId, id);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Profile deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<object>.ErrorResponse(ex.Message));
            }
        }
    }
}
