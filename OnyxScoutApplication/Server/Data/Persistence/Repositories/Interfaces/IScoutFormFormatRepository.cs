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
        Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year, ScoutFormType scoutFormType);

        Task<ActionResult<ScoutFormFormatDto>> GetWithFields(string id);

        Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year, ScoutFormType scoutFormType);

        Task<ActionResult> Update(string id, ScoutFormFormatDto scoutFormFormatDto);
    }
}
