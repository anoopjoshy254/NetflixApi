using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.DTOs;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<ProfileDto>> GetProfilesAsync(Guid userId)
        {
            var profiles = await _profileRepository.GetProfilesByUserIdAsync(userId);
            return profiles.Select(p => new ProfileDto
            {
                Id = p.Id,
                Name = p.Name,
                AvatarUrl = p.AvatarUrl,
                IsKidsProfile = p.IsKidsProfile
            });
        }

        public async Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileDto dto)
        {
            var profiles = await _profileRepository.GetProfilesByUserIdAsync(userId);
            if (profiles.Count() >= 5)
            {
                throw new Exception("Maximum of 5 profiles allowed per account.");
            }

            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = dto.Name,
                AvatarUrl = dto.AvatarUrl,
                IsKidsProfile = dto.IsKidsProfile
            };

            await _profileRepository.AddAsync(profile);

            return new ProfileDto
            {
                Id = profile.Id,
                Name = profile.Name,
                AvatarUrl = profile.AvatarUrl,
                IsKidsProfile = profile.IsKidsProfile
            };
        }

        public async Task<ProfileDto> UpdateProfileAsync(Guid userId, Guid profileId, UpdateProfileDto dto)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            if (profile == null || profile.UserId != userId)
            {
                throw new Exception("Profile not found.");
            }

            if (dto.Name != null) profile.Name = dto.Name;
            if (dto.AvatarUrl != null) profile.AvatarUrl = dto.AvatarUrl;
            if (dto.IsKidsProfile.HasValue) profile.IsKidsProfile = dto.IsKidsProfile.Value;

            await _profileRepository.UpdateAsync(profile);

            return new ProfileDto
            {
                Id = profile.Id,
                Name = profile.Name,
                AvatarUrl = profile.AvatarUrl,
                IsKidsProfile = profile.IsKidsProfile
            };
        }

        public async Task DeleteProfileAsync(Guid userId, Guid profileId)
        {
            var profile = await _profileRepository.GetByIdAsync(profileId);
            if (profile == null || profile.UserId != userId)
            {
                throw new Exception("Profile not found.");
            }

            await _profileRepository.DeleteAsync(profile);
        }
    }
}
