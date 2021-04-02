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

        public ApplicationUserUnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            ApplicationUser = new ApplicationUserRepository(context, mapper);
        }

        public IApplicationUserRepository ApplicationUser { get; }

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
