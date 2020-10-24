using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<ScoutFormFormatDto>>> Get()
        {
            return await unitOfWork.ScoutFormFormats.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScoutFormFormatDto>> Get(int id)
        {
            return await unitOfWork.ScoutFormFormats.GetWithFields(id);
        }

        [HttpGet("ByYear/{year}")]
        public async Task<ActionResult<ScoutFormFormatDto>> GetByYear(int year)
        {
            var v = await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
            return v;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateScoutFormFormat(int id, [FromBody] ScoutFormFormatDto scoutFormForamtModel)
        {
            var response = await unitOfWork.ScoutFormFormats.Update(id, scoutFormForamtModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutFormFormat([FromBody] ScoutFormFormatDto scoutFormForamtModel)
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
