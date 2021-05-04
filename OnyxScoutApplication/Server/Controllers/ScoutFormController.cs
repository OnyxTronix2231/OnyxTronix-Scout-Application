﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<FormDto>> Get(int id)
        {
            var result = await unitOfWork.ScoutForms.GetWithFields(id);
            return result;
        }

        [HttpGet("GetAllByEvent/{eventKey}")]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEvent(string eventKey)
        {
            return await unitOfWork.ScoutForms.GetAllByEvent(eventKey);
        }

        //[HttpGet("ByYear/{year}")]
        //public async Task<ActionResult<ScoutFormDto>> GetByYear(int year)
        //{
        //    return await unitOfWork.ScoutFormFormats.GetWithFieldsByYear(year);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateScoutForm(int id, [FromBody] FormDto formModel)
        {
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
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeam(int teamNumber, string eventKey)
        {
            return await unitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey);
        }
    }
}
