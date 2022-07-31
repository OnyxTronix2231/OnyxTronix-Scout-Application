using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Controllers
{
    [OnyxAuthorize(Role = Role.Scouter)]
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
        public async Task<ActionResult<ScoutFormFormatDto>> Get(string id)
        {
            return await unitOfWork.ScoutFormFormats.GetWithFields(id);
        }

        [HttpGet("ByYear/{year:int}")]
        [HttpGet("ByYear/{year:int}/{scoutFormType}")]
        public async Task<ActionResult<ScoutFormFormatDto>> GetByYear(int year,
            [DefaultValue(ScoutFormType.MainGame)] ScoutFormType scoutFormType)
        {
            var v = await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year, scoutFormType);
            return v;
        }
 
        [HttpGet("TemplateScoutFormByYear/{year:int}")]
        [HttpGet("TemplateScoutFormByYear/{year:int}/{scoutFormType?}")]
        public async Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year,
            [DefaultValue(ScoutFormType.MainGame)] ScoutFormType scoutFormType)
        {
            return await unitOfWork.ScoutFormFormats.GetTemplateScoutFormByYear(year, scoutFormType);
        }

        [OnyxAuthorize(Role = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScoutFormFormat(string id,
            [FromBody] ScoutFormFormatDto scoutFormFormatModel)
        {
            var response = await unitOfWork.ScoutFormFormats.Update(id, scoutFormFormatModel);
            await unitOfWork.Complete();
            return response;
        }

        [OnyxAuthorize(Role = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> CreateScoutFormFormat([FromBody] ScoutFormFormatDto scoutFormFormatModel)
        {
            if (!ModelState.IsValid)
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "Invalid inputs!");
            
            var response = await unitOfWork.ScoutFormFormats.Add(scoutFormFormatModel);
            await unitOfWork.Complete();
            return response;

        }
    }
}
