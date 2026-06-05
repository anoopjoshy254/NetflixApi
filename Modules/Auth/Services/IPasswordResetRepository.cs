using System.Threading.Tasks;
using NetflixApi.Modules.Auth.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public interface IPasswordResetRepository
    {
        Task<PasswordReset?> GetByTokenAsync(string token);
        Task AddAsync(PasswordReset reset);
        Task UpdateAsync(PasswordReset reset);
    }
}
