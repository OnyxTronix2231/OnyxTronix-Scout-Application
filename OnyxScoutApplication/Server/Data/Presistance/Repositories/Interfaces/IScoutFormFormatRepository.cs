using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces
{
    public interface IScoutFormFormatRepository : IRepository<ScoutFormForamt>
    {
        Task<ActionResult<ScoutFormForamt>> GetWithFields(int id);

        Task<ActionResult<ScoutFormForamt>> GetWithFieldsByYear(int year);
    }
}
