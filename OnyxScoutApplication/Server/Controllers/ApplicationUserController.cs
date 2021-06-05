using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Server.Data;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Controllers
{
    [OnyxAuthorize(Role = Role.Owner)]
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserUnitOfWork unitOfWork;

        public ApplicationUserController(IApplicationUserUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUserDto>>> Get()
        {
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
        public async Task<ActionResult> UpdateUser(string name,
            [FromBody] ApplicationUserDto applicationUserDto)
        {
            var response = await unitOfWork.ApplicationUser.Update(name, applicationUserDto);
            if (response is FailResult failResult)
            {
                ModelState.AddModelError("NewPassword", string.Join(" ", failResult.Errors));
                return BadRequest(ModelState);
            }
            await unitOfWork.Complete();
            return response;
        }
    }
}
