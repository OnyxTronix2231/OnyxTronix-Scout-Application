using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Presistance.Repositories;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Presistance.UnitsOfWork
{
    public class ScoutFormFormatUnitOfWork : IScoutFormFormatUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public ScoutFormFormatUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            ScoutFormFormats = new ScoutFormFormatRepository(context);
        }

        public IScoutFormFormatRepository ScoutFormFormats { get; private set; }

        public async Task<ActionResult> Update(int id, ScoutFormForamt scoutFormForamtModel)
        {
            var scoutFormForamtFromDb = await ScoutFormFormats.GetWithFields(id);
            if (scoutFormForamtFromDb.Value == null)
            {
                return new BadRequestObjectResult("No scout from format found to update!");
            }
            scoutFormForamtFromDb.Value.Fields = scoutFormForamtModel.Fields;
            scoutFormForamtFromDb.Value.Year = scoutFormForamtModel.Year; //TODO: Use auto mapper
            context.Update(scoutFormForamtFromDb.Value);
            return new OkResult();
        }

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