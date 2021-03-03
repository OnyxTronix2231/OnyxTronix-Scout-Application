using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using AutoMapper;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ScoutFormFormatUnitOfWork : IScoutFormFormatUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ScoutFormFormatUnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            ScoutFormFormats = new ScoutFormFormatRepository(context, mapper);
        }

        public IScoutFormFormatRepository ScoutFormFormats { get; private set; }

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
