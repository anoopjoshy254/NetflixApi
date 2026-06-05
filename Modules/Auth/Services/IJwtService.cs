using System.Collections.Generic;
using System.Security.Claims;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateExpiredToken(string token);
    }
}
