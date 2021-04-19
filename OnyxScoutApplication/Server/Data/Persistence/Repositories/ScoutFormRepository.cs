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
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormRepository : Repository<ScoutForm, ScoutFormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormDto scoutFormFormat)
        {
            if (await ScoutAppContext.ScoutForms.AnyAsync(i =>
                i.Year == scoutFormFormat.Year && i.MatchName == scoutFormFormat.MatchName && i.TeamNumber == scoutFormFormat.TeamNumber))
            {
                Console.WriteLine("This scout form already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }

            ScoutFormDto clone = new ScoutFormDto
            {
                Year = scoutFormFormat.Year,
                TeamNumber = scoutFormFormat.TeamNumber,
                MatchName = scoutFormFormat.MatchName,
                WriterUserName = scoutFormFormat.WriterUserName
            };
            await base.Add(clone);
            await Context.SaveChangesAsync();
            var result = await ScoutAppContext.ScoutForms.FirstOrDefaultAsync(i =>
                i.Year == scoutFormFormat.Year && i.MatchName == scoutFormFormat.MatchName && i.TeamNumber == scoutFormFormat.TeamNumber);
            scoutFormFormat.Id = result.Id;
            return await Update(result, scoutFormFormat);
        }

        public async Task<ActionResult<ScoutFormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).ThenInclude(i => i.CascadeData)
                .Include(i => i.Data).ThenInclude(i => i.Field).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form found with the id of: " + id);
            }

            return Mapper.Map<ScoutFormDto>(result);
        }

        public async Task<ActionResult> Update(int id, ScoutFormDto scoutFormFormatDto)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).ThenInclude(i => i.CascadeData)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult("No scout from found to update!");
            }

            return await Update(result, scoutFormFormatDto);
        }

        private async Task<ActionResult> Update(ScoutForm scoutForm, ScoutFormDto scoutFormFormatDto)
        {
            var updated = Mapper.Map<ScoutForm>(scoutFormFormatDto);
            scoutForm = Mapper.Map(updated, scoutForm);
            RecursivelySetScoutFormId(scoutForm.Id, scoutForm.Data);
            Context.Update(scoutForm);
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByEvent(string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Include(i => i.Data).ThenInclude(sn => sn.Field)
                .Where(i => i.MatchName.Contains(eventKey)).ToListAsync();
            return Mapper.Map<List<ScoutFormDto>>(scoutForm);
        }

        public async Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Include(i => i.Data).ThenInclude(sn => sn.Field)
                .Where(i => i.TeamNumber == teamNumber && i.MatchName.Contains(eventKey)).ToListAsync();
            return Mapper.Map<List<ScoutFormDto>>(scoutForm);
        }

        private static void RecursivelySetScoutFormId(int id, IEnumerable<ScoutFormData> data)
        {
            foreach (ScoutFormData aData in data)
            {
                aData.ScoutFormId = id;
                RecursivelySetScoutFormId(id, aData.CascadeData);
            }
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
