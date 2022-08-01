using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Firestore;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ScoutFormUnitOfWork : IScoutFormUnitOfWork
    {

        public ScoutFormUnitOfWork(FirestoreDb client, IMapper mapper)
        {
            ScoutForms = new ScoutFormRepository(client, mapper);
        }

        public IScoutFormRepository ScoutForms { get; }

        public async Task<int> Complete()
        {
            //return await context.SaveChangesAsync();
            return 0;
        }

        public void Dispose()
        {
          //  context.Dispose();
        }
    }
}
