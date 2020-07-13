using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnyxScoutApplication.Server.Data.Extensions.Result;
namespace OnyxScoutApplication.Server.Data.Presistance.Repositories
{
    public class ScoutFormFormatRepository : Repository<ScoutFormForamt>, IScoutFormFormatRepository
    {
        public ScoutFormFormatRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormForamt scoutFormForamt)
        {
            if (await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormForamt.Year))
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout format already exists for this year!");
            }
            return await base.Add(scoutFormForamt);
        }

        public async Task<ActionResult<ScoutFormForamt>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.Fields).FirstOrDefaultAsync(i => i.Id == id);
            if(result == null)
            {
                return new NotFoundObjectResult("No scout form format found with the id of: " + id);
            }
            return result;
        }

        public async Task<ActionResult<ScoutFormForamt>> GetWithFieldsByYear(int year)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.Fields).FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }
            return result;
        }

        private ApplicationDbContext ScoutAppContext
        {
            get
            {
                return context as ApplicationDbContext;
            }
        }
    }
}
