using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IScoutFormFormatRepository : IRepository<ScoutFormFormatDto>
    {
        Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year);

        Task<ActionResult<ScoutFormFormatDto>> GetWithFields(int id);

        Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year);

        Task<ActionResult> Update(int id, ScoutFormFormatDto scoutFormFormatDto);
    }
}
