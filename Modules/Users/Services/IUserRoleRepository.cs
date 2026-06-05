using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);
        Task<IEnumerable<string>> GetRolesByUserIdAsync(Guid userId);
    }
}
