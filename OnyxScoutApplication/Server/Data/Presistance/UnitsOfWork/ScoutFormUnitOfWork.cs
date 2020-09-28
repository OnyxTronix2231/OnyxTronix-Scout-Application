using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Presistance.Repositories;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;
using AutoMapper;

namespace OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork
{
    public class ScoutFormUnitOfWork : IScoutFormUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ScoutFormUnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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