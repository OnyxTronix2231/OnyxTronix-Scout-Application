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
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormRepository : Repository<Form, FormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(FormDto formFormat)
        {
            if (await ScoutAppContext.ScoutForms.AnyAsync(i =>
                i.Year == formFormat.Year && i.MatchName == formFormat.MatchName && i.TeamNumber == formFormat.TeamNumber))
            {
                Console.WriteLine("This scout form already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }
            var updated = Mapper.Map<Form>(formFormat);
            Context.Update(updated);
            return await Task.Run(() => new OkResult());
            //await base.Add(clone);
           // await Context.SaveChangesAsync();
            ///var result = await ScoutAppContext.ScoutForms.FirstOrDefaultAsync(i =>
          //      i.Year == formFormat.Year && i.MatchName == formFormat.MatchName && i.TeamNumber == formFormat.TeamNumber);
        //    formFormat.Id = result.Id;
          //  return await Update(result, formFormat);
        }

        public async Task<ActionResult<FormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.
                Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.CascadeData).ThenInclude(i => i.Field)
                .Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.Field).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form found with the id of: " + id);
            }

            return Mapper.Map<FormDto>(result);
        }

        public async Task<ActionResult> Update(int id, FormDto formFormatDto)
        {
            // var result = await ScoutAppContext.ScoutForms.Include(i => i.FieldsInStages).ThenInclude(i => i.FormData)
            //     .ThenInclude(i => i.CascadeData).FirstOrDefaultAsync(i => i.Id == id);
            // if (result == null)
            // {
            //     return new BadRequestObjectResult("No scout from found to update!");
            // }

            return await Update(formFormatDto);
        }

        private async Task<ActionResult> Update(Form form, FormDto formFormatDto)
        {
          //  var updated = Mapper.Map<ScoutForm>(scoutFormFormatDto);
           // scoutForm = Mapper.Map(updated, scoutForm);
           // RecursivelySetScoutFormId(scoutForm.Id, scoutForm.Data);
            Context.Update(form);
            return await Task.Run(() => new OkResult());
        }
        
        private async Task<ActionResult> Update(FormDto formFormatDto)
        {
            //  var updated = Mapper.Map<ScoutForm>(scoutFormFormatDto);
            // scoutForm = Mapper.Map(updated, scoutForm);
            // RecursivelySetScoutFormId(scoutForm.Id, scoutForm.Data);
            var entity = Mapper.Map<Form>(formFormatDto);
            Context.Update(entity);
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEvent(string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(sn => sn.Field).Where(i => i.MatchName.Contains(eventKey)).ToListAsync();
             return Mapper.Map<List<FormDto>>(scoutForm);
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(sn => sn.Field).Where(i => i.TeamNumber == teamNumber && i.MatchName.Contains(eventKey))
                .ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        // private static void RecursivelySetScoutFormId(int id, IEnumerable<FormData> data)
        // {
        //     foreach
        // (FormData aData in data)
        //     {
        //         aData.ScoutFormId = id;
        //         RecursivelySetScoutFormId(id, aData.CascadeData);
        //     }
        // }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
