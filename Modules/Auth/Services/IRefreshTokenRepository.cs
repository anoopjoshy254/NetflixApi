using System.Threading.Tasks;
using NetflixApi.Modules.Auth.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
