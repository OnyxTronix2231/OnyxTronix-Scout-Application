using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces
{
    public interface IScoutFormFormatUnitOfWork : IDisposable
    {
        IScoutFormFormatRepository ScoutFormFormats { get; }
        Task<ActionResult> Update(int id, ScoutFormFormat scoutFormForamtModel);
        Task<int> Complete();
    }
}