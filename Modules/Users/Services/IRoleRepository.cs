using System;
using System.Threading.Tasks;
using NetflixApi.Modules.Users.Models;

namespace NetflixApi.Modules.Users.Services
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name);
        Task AddAsync(Role role);
    }
}
