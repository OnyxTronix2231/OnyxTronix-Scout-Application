using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormRepository : Repository<Form, FormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByType(ScoutFormType scoutFormType)
        {
            var forms = await ScoutAppContext.ScoutForms.Where(i => i.Type == scoutFormType).ToListAsync();
            return Mapper.Map<List<FormDto>>(forms);
        }
        
        public override async Task<ActionResult> Add(FormDto form)
        {
            if (await ScoutAppContext.ScoutForms.AnyAsync(i =>
                i.Year == form.Year && i.KeyName == form.KeyName &&
                i.TeamNumber == form.TeamNumber))
            {
                Console.WriteLine("This scout form already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }

            var updated = Mapper.Map<Form>(form);
            Context.Update(updated);
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<FormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.WithAllData().FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form found with the id of: " + id);
            }

            result.FormDataInStages = result.FormDataInStages.OrderBy(i => i.Index).ToList();
            return Mapper.Map<FormDto>(result);
        }

        public async Task<ActionResult> Update(int id, FormDto formFormatDto)
        {
            var entity = Mapper.Map<Form>(formFormatDto);
            Context.Update(entity);
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Where(i => i.KeyName.Contains(eventKey))
                .WithAllData().ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        public async Task<ActionResult<FormDto>> GetByTeamAndKey(int teamNumber, string key,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.AsNoTracking().SingleAsync(i => i.TeamNumber == teamNumber
                                                                        && i.KeyName.Equals(key)
                                                                        && i.Type == scoutFormType);
            return Mapper.Map<FormDto>(scoutForm);
        }
        
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEvent(string eventKey,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Where(i => i.KeyName.Contains(eventKey)).ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.WithAllData()
                .Where(i => i.TeamNumber == teamNumber && i.KeyName.Equals(eventKey))
                .ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        public override async Task UpdateFromTracking(FormDto obj)
        {
            Context.Entry(Mapper.Map<Form>(obj)).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        
        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
