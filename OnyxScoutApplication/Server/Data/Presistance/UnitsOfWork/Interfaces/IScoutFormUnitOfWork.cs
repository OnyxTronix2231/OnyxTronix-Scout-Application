using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces
{
    public interface IScoutFormUnitOfWork : IDisposable
    {
        IScoutFormRepository ScoutForms { get; }
        Task<int> Complete();
    }
}