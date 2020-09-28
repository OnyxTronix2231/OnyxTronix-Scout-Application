using AutoMapper;
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
    public class ScoutFormRepository : Repository<ScoutForm, ScoutFormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormDto scoutFormForamt)
        {
            if (await ScoutAppContext.ScoutForms.AnyAsync(i => i.Year == scoutFormForamt.Year && i.MatchName == scoutFormForamt.MatchName && i.TeamNumber == scoutFormForamt.TeamNumber))
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }
            return await base.Add(scoutFormForamt);
        }

        public async Task<ActionResult<ScoutFormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Id == id);
            if(result == null)
            {
                return new NotFoundObjectResult("No scout form found with the id of: " + id);
            }
            return mapper.Map<ScoutFormDto>(result);
        }

        public async Task<ActionResult<ScoutFormDto>> GetWithFieldsByYear(int year)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form found for year - " + year);
            }
            return mapper.Map<ScoutFormDto>(result);
        }

        public async Task<ActionResult> Update(int id, ScoutFormDto scoutFormForamtDto)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult("No scout from found to update!");
            }
            var updated = mapper.Map<ScoutFormFormat>(scoutFormForamtDto);
            result = mapper.Map(updated, result);
            context.Update(result);
            return new OkResult();
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
