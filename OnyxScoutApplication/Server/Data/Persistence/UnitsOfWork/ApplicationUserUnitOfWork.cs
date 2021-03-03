using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using AutoMapper;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ApplicationUserUnitOfWork : IApplicationUserUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ApplicationUserUnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            ApplicationUser = new ApplicationUserRepository(context, mapper);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }

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
