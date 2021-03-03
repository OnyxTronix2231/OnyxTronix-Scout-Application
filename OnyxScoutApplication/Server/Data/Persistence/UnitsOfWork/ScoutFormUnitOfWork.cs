using System.Threading.Tasks;
using AutoMapper;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ScoutFormUnitOfWork : IScoutFormUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public ScoutFormUnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            ScoutForms = new ScoutFormRepository(context, mapper);
        }

        public IScoutFormRepository ScoutForms { get; private set; }

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
