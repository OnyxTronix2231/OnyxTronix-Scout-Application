using System.Threading.Tasks;
using AutoMapper;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class CustomEventUnitOfWork : ICustomEventUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public CustomEventUnitOfWork(ApplicationDbContext context, 
            IMapper mapper)
        {
            this.context = context;
            CustomEvents = new CustomEventRepository(context, mapper);
        }

        public ICustomEventRepository CustomEvents { get; }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
