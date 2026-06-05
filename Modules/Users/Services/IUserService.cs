using System;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.DTOs;

namespace NetflixApi.Modules.Users.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto dto);
        Task SoftDeleteUserAsync(Guid userId);
    }
}
