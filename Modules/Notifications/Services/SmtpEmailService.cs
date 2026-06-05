using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetflixApi.Modules.Notifications.Interfaces;
using NetflixApi.Modules.Notifications.Models;

namespace NetflixApi.Modules.Notifications.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IOptions<EmailSettings> settings, ILogger<SmtpEmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.Email, "Netflix Clone Support"),
                    Subject = subject,
                    Body = htmlContent,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                using var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_settings.Email, _settings.Password),
                    EnableSsl = true,
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while sending email via SMTP.");
            }
        }
    }
}
