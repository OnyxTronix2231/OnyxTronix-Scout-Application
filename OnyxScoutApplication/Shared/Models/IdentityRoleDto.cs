using System;
using System.Collections.Generic;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class IdentityRoleDto
    {
        public virtual string Id { get; set; }
        
        public virtual string Name { get; set; }
       
        public virtual string NormalizedName { get; set; }
    }
}
