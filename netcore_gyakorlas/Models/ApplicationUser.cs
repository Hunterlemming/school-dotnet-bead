using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace netcore_gyakorlas.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool Enabled { get; set; }

        public DateTime DoB { get; set; }

        public virtual ICollection<IdentityUserRole<int>> Roles { get; } = new List<IdentityUserRole<int>>();
    }
}