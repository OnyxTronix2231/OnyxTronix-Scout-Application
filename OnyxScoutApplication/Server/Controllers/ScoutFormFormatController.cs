using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScoutFormFormatController : Controller
    {
        private readonly IScoutFormFormatUnitOfWork unitOfWork;

        public ScoutFormFormatController(IScoutFormFormatUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoutFormForamt>>> Get()
        {
            return await unitOfWork.ScoutFormFormats.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScoutFormForamt>> Get(int id)
        {
            return await unitOfWork.ScoutFormFormats.GetWithFields(id);
        }

        [HttpGet("ByYear/{year}")]
        public async Task<ActionResult<ScoutFormForamt>> GetByYear(int year)
        {
            return await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateScoutFormFormat(int id, [FromBody] ScoutFormForamt scoutFormForamtModel)
        {
            var response = await unitOfWork.Update(id, scoutFormForamtModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutFormFormat([FromBody] ScoutFormForamt scoutFormForamtModel)
        {
            if (ModelState.IsValid)
            {
                var response = await unitOfWork.ScoutFormFormats.Add(scoutFormForamtModel);
                await unitOfWork.Complete();
                return response;
            }
            return ResultCode(System.Net.HttpStatusCode.BadRequest, "Invalid inputs!");
        }
    }
}
