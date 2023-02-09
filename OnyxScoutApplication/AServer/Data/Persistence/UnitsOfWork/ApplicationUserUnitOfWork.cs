using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Server.Models;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ApplicationUserUnitOfWork : IApplicationUserUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public ApplicationUserUnitOfWork(ApplicationDbContext context, IMapper mapper,  
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            ApplicationUser = new ApplicationUserRepository(context, mapper, userManager);
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
