using System.Threading.Tasks;

namespace NetflixApi.Modules.Notifications.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    }
}
