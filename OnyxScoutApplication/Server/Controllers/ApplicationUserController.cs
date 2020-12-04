using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Server.Data;
using IdentityModel;
using Microsoft.EntityFrameworkCore;

namespace OnyxScoutApplication.Server.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IApplicationUserUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ApplicationUserController(ApplicationDbContext applicationDbContext,IApplicationUserUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUserDto>>> Get()
        {
            //var u = await userManager.GetUserAsync(User);
            //var r = await userManager.GetRolesAsync(u);
            //var vv = applicationDbContext.Users.Include(u => u.UserRoles).ThenInclude(i => i.Role).ToList();
            var v = await unitOfWork.ApplicationUser.GetAllWithRoles();
            return v;
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<List<ApplicationRoleDto>>> GetAllRoles()
        {
            var v = await unitOfWork.ApplicationUser.GetAllRoles();
            return v;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ApplicationUserDto>> GetByName(string name)
        {
            var v = await unitOfWork.ApplicationUser.GetByNameWithRoles(name);
            return v;
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> UpdateScoutFormFormat(string name, [FromBody] ApplicationUserDto applicationUserDto)
        {
            var response = await unitOfWork.ApplicationUser.Update(name, applicationUserDto);
            await unitOfWork.Complete();
            return response;
        }
    }
}
