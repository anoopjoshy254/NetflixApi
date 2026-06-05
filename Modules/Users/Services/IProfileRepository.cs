using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetProfilesByUserIdAsync(Guid userId);
        Task<Profile?> GetByIdAsync(Guid id);
        Task AddAsync(Profile profile);
        Task UpdateAsync(Profile profile);
        Task DeleteAsync(Profile profile);
    }
}
