using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetflixApi.Modules.Reports.DTOs;

namespace NetflixApi.Modules.Reports.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportDto>> GetRevenueReportAsync(DateTime from, DateTime to);
        Task<IEnumerable<SubscriptionStatusReportDto>> GetSubscriptionsReportAsync();
        Task<IEnumerable<UserRegistrationReportDto>> GetUsersReportAsync(int days);
        Task<IEnumerable<ViewingReportDto>> GetViewingReportAsync();
    }
}
