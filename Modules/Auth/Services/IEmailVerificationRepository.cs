using System.Threading.Tasks;
using NetflixApi.Modules.Auth.Models;

namespace NetflixApi.Modules.Auth.Services
{
    public interface IEmailVerificationRepository
    {
        Task<EmailVerification?> GetByTokenAsync(string token);
        Task AddAsync(EmailVerification verification);
        Task UpdateAsync(EmailVerification verification);
    }
}
