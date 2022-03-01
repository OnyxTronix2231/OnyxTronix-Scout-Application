using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ScoutFormController : Controller
    {
        private readonly IScoutFormUnitOfWork unitOfWork;

        public ScoutFormController(IScoutFormUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormDto>>> Get()
        {
            return await unitOfWork.ScoutForms.GetAll();
        }
        
        [HttpGet("GetAllByType/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> Get(ScoutFormType scoutFormType)
        {
            return await unitOfWork.ScoutForms.GetAllByType(scoutFormType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormDto>> Get(int id)
        {
            var result = await unitOfWork.ScoutForms.GetWithFields(id);
            return result;
        }

        [HttpPost("SaveImage")]
        public async Task<ActionResult> SaveImage([FromForm] IEnumerable<IFormFile> files)
        {
            long maxFileSize = 1024 * 1024 * 15;
           // var form = await unitOfWork.ScoutForms.GetByTeamAndKey(teamNumber, keyName, ScoutFormType.Pit);
            Console.WriteLine("OKKKKKKKKKK");
          //  Console.WriteLine(form.Value.KeyName);
          //  Console.WriteLine(form.Value.TeamNumber);
            Console.WriteLine(files.Count());
            Console.WriteLine(files.ElementAt(0).Name);
            Console.WriteLine(files.ElementAt(0).FileName);
            return new OkResult();
        }

        [HttpGet("GetAllByEvent/{eventKey}")]
        [HttpGet("GetAllByEvent/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>>
            GetAllByEvent(string eventKey, ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByEvent(eventKey, scoutFormType);
        }
        
        [HttpGet("GetAllByEventWithData/{eventKey}")]
        [HttpGet("GetAllByEventWithData/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey,
            ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByEventWithData(eventKey, scoutFormType);
        }

        //[HttpGet("ByYear/{year}")]
        //public async Task<ActionResult<ScoutFormDto>> GetByYear(int year)
        //{
        //    return await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScoutForm(int id, [FromBody] FormDto formModel)
        {
            if (!User.IsInRole(Role.Admin.ToString()) && User.GetDisplayName() != formModel.WriterUserName)
            {
                return new UnauthorizedObjectResult($"Only {Role.Admin.ToString()} or" +
                                                    $" {formModel.WriterUserName} can edit this form!");
            }
            var response = await unitOfWork.ScoutForms.Update(id, formModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> CreateScoutForm([FromBody] FormDto formModel)
        {
            if (!ModelState.IsValid) 
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "Invalid inputs!");
            
            var response = await unitOfWork.ScoutForms.Add(formModel);
            await unitOfWork.Complete();
            return response;

        }

        [HttpGet("GetAllByTeam/{teamNumber}/{eventKey}")]
        [HttpGet("GetAllByTeam/{teamNumber}/{eventKey}/{scoutFormType}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeam(int teamNumber, string eventKey,
            ScoutFormType scoutFormType = ScoutFormType.MainGame)
        {
            return await unitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey, scoutFormType);
        }
    }
}
