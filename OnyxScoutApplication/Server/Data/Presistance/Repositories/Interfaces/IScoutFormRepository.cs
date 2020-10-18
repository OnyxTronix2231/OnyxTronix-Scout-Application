using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces
{
    public interface IScoutFormRepository : IRepository<ScoutForm, ScoutFormDto>
    {
        Task<ActionResult<ScoutFormDto>> GetWithFields(int id);

        Task<ActionResult<ScoutFormDto>> GetWithDataByYear(int year);
        Task<ActionResult> Update(int id, ScoutFormDto scoutFormForamtDto);
        Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey);
    }
}
