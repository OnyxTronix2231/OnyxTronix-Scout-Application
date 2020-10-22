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
    public class ScoutFormFormatRepository : Repository<ScoutFormFormat, ScoutFormFormatDto>, IScoutFormFormatRepository
    {
        public ScoutFormFormatRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormFormatDto scoutFormForamt)
        {
            if (await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormForamt.Year))
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout format already exists for this year!");
            }
            ScoutFormFormatDto clone = new ScoutFormFormatDto()
            {
                Year = scoutFormForamt.Year
            };
            await base.Add(clone);
            await context.SaveChangesAsync();
            var result = await ScoutAppContext.ScoutFormFormats.FirstOrDefaultAsync(i => i.Year == scoutFormForamt.Year);
            scoutFormForamt.Id = result.Id;
            return await Update(result.Id, scoutFormForamt);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.Fields).FirstOrDefaultAsync(i => i.Id == id);
            if(result == null)
            {
                return new NotFoundObjectResult("No scout form format found with the id of: " + id);
            }
            return mapper.Map<ScoutFormFormatDto>(result);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.Fields).FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }
            return mapper.Map<ScoutFormFormatDto>(result);
        }

        public async Task<ActionResult> Update(int id, ScoutFormFormatDto scoutFormForamtDto)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.Fields).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult("No scout from format found to update!");
            }
            var updated = mapper.Map<ScoutFormFormat>(scoutFormForamtDto);
            result = mapper.Map(updated, result);
            RecursivelySetScoutFormForamtId(id, result.Fields);
            context.Update(result);
            return new OkResult();
        }

        private void RecursivelySetScoutFormForamtId(int id, List<Field> fields)
        {
            foreach (Field aField in fields)
            {
                aField.ScoutFormForamtId = id;
                RecursivelySetScoutFormForamtId(id, aField.CascadeFields);
            }
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
