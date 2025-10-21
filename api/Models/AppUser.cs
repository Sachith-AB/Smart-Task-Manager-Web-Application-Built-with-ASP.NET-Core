using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        // These properties come from IdentityUser:
        // - Id (string)
        // - UserName
        // - Email
        // - PasswordHash
        // - PhoneNumber

        // Custom properties not from IdentityUser:
        public string FullName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        public AppRole? Role { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}