using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces
{
    public interface IScoutFormFormatRepository : IRepository<ScoutFormFormat>
    {
        Task<ActionResult<ScoutFormFormat>> GetWithFields(int id);

        Task<ActionResult<ScoutFormFormat>> GetWithFieldsByYear(int year);
    }
}
