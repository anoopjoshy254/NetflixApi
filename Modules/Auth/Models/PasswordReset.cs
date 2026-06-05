using System;

namespace NetflixApi.Modules.Auth.Models
{
    public class PasswordReset
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; }
    }
}
