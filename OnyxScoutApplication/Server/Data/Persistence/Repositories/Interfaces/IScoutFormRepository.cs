﻿using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IScoutFormRepository : IRepository<FormDto>
    {
        Task<ActionResult<FormDto>> GetWithFields(string id);

        Task<ActionResult> Update(string id, FormDto scoutFormDto);
        Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey,
            ScoutFormType scoutFormType);
        Task<ActionResult<IEnumerable<SimpleFormDto>>> GetAllByEvent(string eventKey, ScoutFormType scoutFormType);
        Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey, ScoutFormType scoutFormType);
        Task<ActionResult<IEnumerable<FormDto>>> GetAllByType(ScoutFormType scoutFormType);
        Task<ActionResult<FormDto>> GetByTeamAndKey(int teamNumber, string key,
            ScoutFormType scoutFormType);
    }
}
