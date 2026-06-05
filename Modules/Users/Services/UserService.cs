using System;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.DTOs;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }
            return MapToDto(user);
        }

        public async Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;
            
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);

            return MapToDto(user);
        }

        public async Task SoftDeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || !user.IsActive)
            {
                throw new Exception("User not found.");
            }

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
        }

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                IsEmailVerified = user.IsEmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
