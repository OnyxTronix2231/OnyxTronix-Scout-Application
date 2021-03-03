using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, ApplicationUserDto>
    {
        Task<ActionResult<List<ApplicationRoleDto>>> GetAllRoles();
        Task<ActionResult<List<ApplicationUserDto>>> GetAllWithRoles();
        Task<ActionResult<ApplicationUserDto>> GetByNameWithRoles(string name);
        Task<ActionResult> Update(string name, ApplicationUserDto applicationUserDto);
    }
}
