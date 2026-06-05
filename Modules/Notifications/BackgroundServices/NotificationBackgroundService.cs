using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetflixApi.Data;
using NetflixApi.Modules.Notifications.Interfaces;

namespace NetflixApi.Modules.Notifications.BackgroundServices
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(IServiceProvider serviceProvider, ILogger<NotificationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    // Run once daily at 09:00
                    var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
                    if (now > nextRunTime)
                    {
                        nextRunTime = nextRunTime.AddDays(1);
                    }

                    var delay = nextRunTime - now;
                    _logger.LogInformation("NotificationBackgroundService waiting for {Delay} until next run.", delay);
                    await Task.Delay(delay, stoppingToken);

                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    var expiringSubscriptions = await dbContext.UserSubscriptions
                        .Where(s => s.Status == "Active" && s.EndDate <= DateTime.UtcNow.AddDays(3) && s.EndDate > DateTime.UtcNow)
                        .ToListAsync(stoppingToken);

                    foreach (var sub in expiringSubscriptions)
                    {
                        var daysLeft = (sub.EndDate - DateTime.UtcNow).Days;
                        await notificationService.SendSubscriptionExpiryAlertAsync(sub.UserId, daysLeft);
                    }

                    _logger.LogInformation("Processed expiry notifications for {Count} users.", expiringSubscriptions.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing NotificationBackgroundService.");
                }

                // Prevent immediate re-run if execution is fast
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
