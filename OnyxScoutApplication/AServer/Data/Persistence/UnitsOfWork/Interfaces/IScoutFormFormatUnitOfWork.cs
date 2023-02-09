using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces
{
    public interface IScoutFormFormatUnitOfWork : IDisposable
    {
        IScoutFormFormatRepository ScoutFormFormats { get; }
    }
}
