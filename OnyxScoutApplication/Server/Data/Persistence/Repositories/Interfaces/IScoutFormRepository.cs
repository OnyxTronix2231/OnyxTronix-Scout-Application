using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IScoutFormRepository : IRepository<ScoutFormDto>
    {
        Task<ActionResult<ScoutFormDto>> GetWithFields(int id);

        Task<ActionResult> Update(int id, ScoutFormDto scoutFormFormatDto);
        Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey);
        Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByEvent(string eventKey);
    }
}
