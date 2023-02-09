using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Profiles;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser, ApplicationUserDto>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserRepository(ApplicationDbContext context, IMapper mapper,
            UserManager<ApplicationUser> userManager) : base(context, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<ActionResult<List<ApplicationUserDto>>> GetAllWithRoles()
        {
            var v = await ScoutAppContext.Users.Include(u => u.UserRoles).ThenInclude(i => i.Role).ToListAsync();
            return Mapper.Map<List<ApplicationUserDto>>(v);
        }

        public async Task<ActionResult<ApplicationUserDto>> GetByNameWithRoles(string name)
        {
            var v = await ScoutAppContext.Users.AsQueryable().Include(u => u.UserRoles).ThenInclude(i => i.Role)
                .FirstOrDefaultAsync(i => i.UserName == name);
            return Mapper.Map<ApplicationUserDto>(v);
        }

        public async Task<ActionResult<List<ApplicationRoleDto>>> GetAllRoles()
        {
            var v = await ScoutAppContext.Roles.AsQueryable().ToListAsync();
            return Mapper.Map<List<ApplicationRoleDto>>(v);
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

        private async Task<ActionResult> Update(ApplicationUser applicationUser, ApplicationUserDto applicationUserDto)
        {
            if (!string.IsNullOrWhiteSpace(applicationUserDto.NewPassword))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(applicationUser);
                var resetPasswordResult = await userManager.
                    ResetPasswordAsync(applicationUser, token, applicationUserDto.NewPassword);
                if (resetPasswordResult.Errors.Any())
                {
                    return new FailResult(HttpStatusCode.BadRequest,
                        resetPasswordResult.Errors.Select(i => i.Description).ToList());
                }
            }

            Mapper.Map(applicationUserDto, applicationUser);
            foreach (var userRole in applicationUser.UserRoles)
            {
                userRole.Role = null;
            }

            Context.Update(applicationUser);
            return await Task.Run(() => new OkResult());
        }


        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
