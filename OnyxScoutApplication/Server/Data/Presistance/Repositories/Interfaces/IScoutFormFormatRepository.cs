using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces
{
    public interface IScoutFormFormatRepository : IRepository<ScoutFormFormat, ScoutFormFormatDto>
    {
        Task<ActionResult<ScoutFormFormatDto>> GetWithFields(int id);

        Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year);
        Task<ActionResult> Update(int id, ScoutFormFormatDto scoutFormForamtDto);
    }
}
