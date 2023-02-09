using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();

        [NotMapped]
        public string NewPassword { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        // public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public ApplicationUser User { get; set; }

        [ForeignKey("ApplicationRoleId")]
        public ApplicationRole Role { get; set; }

        public string ApplicationRoleId
        {
            get => RoleId;
            set => RoleId = value;
        }
    }
}
