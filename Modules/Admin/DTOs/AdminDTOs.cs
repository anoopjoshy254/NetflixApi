using System.Collections.Generic;

namespace NetflixApi.Modules.Admin.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveSubscriptions { get; set; }
        public decimal TotalRevenueThisMonth { get; set; }
        public int TotalContentCount { get; set; }
    }

    public class UserListResponseDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string SubscriptionStatus { get; set; }
    }

    public class AdminSubscriptionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string PlanName { get; set; }
        public string Status { get; set; }
    }
}
