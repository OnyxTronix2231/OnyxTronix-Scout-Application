using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OnyxScoutApplication.Shared.Models
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<ApplicationUserRoleDto> UserRoles { get; set; } = new List<ApplicationUserRoleDto>();

    }

    public class ApplicationRoleDto
    {
        public string Id { get; set; }
       // public virtual List<ApplicationUserRoleDto> UserRoles { get; set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }

    public class ApplicationUserRoleDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUserDto User { get; set; }
        public string RoleId { get; set; }
        public ApplicationRoleDto Role { get; set; }
    }
}
