using System;

namespace NetflixApi.Modules.Users.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
