using System.Collections.Generic;

namespace NetflixApi.Modules.Reports.DTOs
{
    public class RevenueReportDto
    {
        public string PlanName { get; set; }
        public decimal TotalRevenue { get; set; }
        public int SubscriptionCount { get; set; }
    }

    public class SubscriptionStatusReportDto
    {
        public string PlanName { get; set; }
        public int Active { get; set; }
        public int Cancelled { get; set; }
        public int Expired { get; set; }
    }

    public class UserRegistrationReportDto
    {
        public string Date { get; set; }
        public int NewUsersCount { get; set; }
    }

    public class ViewingReportDto
    {
        public int ContentId { get; set; }
        public int WatchCount { get; set; }
    }
}
