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
                i.Year == formFormat.Year && i.MatchName == formFormat.MatchName &&
                i.TeamNumber == formFormat.TeamNumber))
            {
                Console.WriteLine("This scout form already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }

            var updated = Mapper.Map<Form>(formFormat);
            Context.Update(updated);
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<FormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.WithAllFields().FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEvent(string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.WithAllFields()
                .Where(i => i.MatchName.Contains(eventKey)).ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.WithAllFields()
                .Where(i => i.TeamNumber == teamNumber && i.MatchName.Contains(eventKey))
                .ToListAsync();
            return Mapper.Map<List<FormDto>>(scoutForm);
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
