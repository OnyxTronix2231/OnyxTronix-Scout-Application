using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class CustomEventUnitOfWork : ICustomEventUnitOfWork
    {

        public CustomEventUnitOfWork(FirestoreDb client, IMapper mapper)
        {
            CustomEvents = new CustomEventRepository(client, mapper);
        }

        public ICustomEventRepository CustomEvents { get; }

        public void Dispose()
        {
        }
    }
}
