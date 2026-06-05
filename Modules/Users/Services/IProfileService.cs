using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.DTOs;

namespace NetflixApi.Modules.Users.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileDto>> GetProfilesAsync(Guid userId);
        Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileDto dto);
        Task<ProfileDto> UpdateProfileAsync(Guid userId, Guid profileId, UpdateProfileDto dto);
        Task DeleteProfileAsync(Guid userId, Guid profileId);
    }
}
