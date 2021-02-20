using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser, ApplicationUserDto>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ActionResult<List<ApplicationUserDto>>> GetAllWithRoles()
        {
            var v = await ScoutAppContext.Users.Include(u => u.UserRoles).ThenInclude(i => i.Role).ToListAsync();
            return mapper.Map<List<ApplicationUserDto>>(v);
        }

        public async Task<ActionResult<ApplicationUserDto>> GetByNameWithRoles(string name)
        {
            var v = await ScoutAppContext.Users.Include(u => u.UserRoles).ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i => i.UserName == name);
            return mapper.Map<ApplicationUserDto>(v);
        }

        public async Task<ActionResult<List<ApplicationRoleDto>>> GetAllRoles()
        {
            var v = await ScoutAppContext.Roles.ToListAsync();
            return mapper.Map<List<ApplicationRoleDto>>(v);
        }

        public async Task<ActionResult> Update(string name, ApplicationUserDto applicationUserDto)
        {
            var result = await ScoutAppContext.Users.Include(u => u.UserRoles).ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i => i.UserName == name);
            if (result == null)
            {
                return new BadRequestObjectResult($"No user with name: {name} found to update!");
            }

            return await Update(result, applicationUserDto);
        }

        public async Task<ActionResult> Update(ApplicationUser applicationUser, ApplicationUserDto applicationUserDto)
        {
            var updated = mapper.Map(applicationUserDto, applicationUser);
            foreach (var userRole in applicationUser.UserRoles)
            {
                userRole.Role = null;
            }

            context.Update(applicationUser);
            return await Task.Run(() => new OkResult());
        }


        private ApplicationDbContext ScoutAppContext
        {
            get { return context as ApplicationDbContext; }
        }
    }
}
