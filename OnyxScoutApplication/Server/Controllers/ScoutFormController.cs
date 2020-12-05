using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Controllers
{
    [Authorize(Roles = Roles.Scouter)]
    [ApiController]
    [Route("[controller]")]
    public class ScoutFormController : Controller
    {
        private readonly IScoutFormUnitOfWork unitOfWork;

        public ScoutFormController(IScoutFormUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoutFormDto>>> Get()
        {
            return await unitOfWork.ScoutForms.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScoutFormDto>> Get(int id)
        {
            return await unitOfWork.ScoutForms.GetWithFields(id);
        }

        //[HttpGet("ByYear/{year}")]
        //public async Task<ActionResult<ScoutFormDto>> GetByYear(int year)
        //{
        //    return await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScoutForm(int id, [FromBody] ScoutFormDto scoutFormModel)
        {
            var response = await unitOfWork.ScoutForms.Update(id, scoutFormModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutForm([FromBody] ScoutFormDto scoutFormModel)
        {
            if (ModelState.IsValid)
            {
                var response = await unitOfWork.ScoutForms.Add(scoutFormModel);
                await unitOfWork.Complete();
                return response;
            }
            return ResultCode(System.Net.HttpStatusCode.BadRequest, "Invalid inputs!");
        }

        [HttpGet("GetAllByTeam/{teamNumber}/{eventkey}")]
        public async Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByTeam(int teamNumber, string eventKey)
        {
            return await unitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey);
        }
    }
}
