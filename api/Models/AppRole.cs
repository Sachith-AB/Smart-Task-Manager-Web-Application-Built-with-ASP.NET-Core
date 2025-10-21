using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppRole : IdentityRole
    {
        // These properties come from IdentityRole:
        // - Id (string) - Unique identifier for the role
        // - Name (string) - Role name (e.g., "Admin", "User", "Manager")
        // - NormalizedName (string) - Normalized role name for searches
        // - ConcurrencyStamp (string) - Token for concurrency control

        // You can add custom properties here if needed
    }
}