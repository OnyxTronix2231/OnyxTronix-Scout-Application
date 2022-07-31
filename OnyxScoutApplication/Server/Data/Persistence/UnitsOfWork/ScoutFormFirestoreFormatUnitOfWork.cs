using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using AutoMapper;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ScoutFormFaunaFormatUnitOfWork : IScoutFormFormatUnitOfWork
    {
        private readonly FirestoreDb client;

        public ScoutFormFaunaFormatUnitOfWork(FirestoreDb client, IMapper mapper)
        {
            this.client = client;
            ScoutFormFormats = new ScoutFormFormatFirestoreRepository(client, mapper);
        }

        public IScoutFormFormatRepository ScoutFormFormats { get; }

        public async Task<int> Complete()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
