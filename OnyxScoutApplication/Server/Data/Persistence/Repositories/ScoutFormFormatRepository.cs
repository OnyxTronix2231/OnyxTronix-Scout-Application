using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormFormatRepository : Repository<ScoutFormFormat, ScoutFormFormatDto>, IScoutFormFormatRepository
    {
        public ScoutFormFormatRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormFormatDto scoutFormFormat)
        {
            if (await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormFormat.Year))
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest,
                    "This scout format already exists for this year!");
            }

            ScoutFormFormatDto clone = new ScoutFormFormatDto
            {
                Year = scoutFormFormat.Year
            };
            return await base.Add(scoutFormFormat);
            //await Context.SaveChangesAsync();
          //  var result =
         ////       await ScoutAppContext.ScoutFormFormats.FirstOrDefaultAsync(i => i.Year == scoutFormFormat.Year);
          //  scoutFormFormat.Id = result.Id;
         //   return await Update(result.Id, scoutFormFormat);
        }

        public async Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year)
        {
            var result = await GetWithFieldsByYear(year);
            if (result.Value == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }

            return Mapper.Map<FormDto>(result.Value);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.FieldsInStages).
                ThenInclude(f => f.Fields).ThenInclude(i => i.CascadeFields).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found with the id of: " + id);
            }

            //result.Fields = result.Fields.OrderBy(i => i.Index).ToList();
            return Mapper.Map<ScoutFormFormatDto>(result);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year)
        {
            var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.FieldsInStages).
                ThenInclude(f => f.Fields).ThenInclude(i => i.CascadeFields).FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }

           // result.Fields = result.Fields.OrderBy(i => i.Index).ToList();
            return Mapper.Map<ScoutFormFormatDto>(result);
        }

        public async Task<ActionResult> Update(int id, ScoutFormFormatDto scoutFormFormatDto)
        {
            // var result = await ScoutAppContext.ScoutFormFormats.Include(i => i.FieldsInStages).
            //     ThenInclude(f => f.Fields).ThenInclude(i => i.CascadeFields).FirstOrDefaultAsync(i => i.Id == id);
            if (!await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormFormatDto.Year))
            {
                return new BadRequestObjectResult("No scout from format found to update!");
            }
            var updated = Mapper.Map<ScoutFormFormat>(scoutFormFormatDto);
            Context.Update(updated);
            return await Task.Run(() => new OkResult());
            //return await Update(result, scoutFormFormatDto);
        }

        private async Task<ActionResult> Update(ScoutFormFormat scoutFormFormat, ScoutFormFormatDto scoutFormFormatDto)
        {
            var updated = Mapper.Map<ScoutFormFormat>(scoutFormFormatDto);
            scoutFormFormat = Mapper.Map(updated, scoutFormFormat);
            //RecursivelySetScoutFormFormatId(scoutFormFormat.Id, scoutFormFormat.FieldsInStages);
            Context.Update(scoutFormFormat);
            return await Task.Run(() => new OkResult());
        }

        private static void RecursivelySetScoutFormFormatId(int id, List<FieldsInStage> fieldsInStages)
        {
            // foreach (Field aField in fieldsInStages)
            // {
            //     aField.ScoutFormFormatId = id;
            //     RecursivelySetScoutFormFormatId(id, aField.CascadeFields);
            // }
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
