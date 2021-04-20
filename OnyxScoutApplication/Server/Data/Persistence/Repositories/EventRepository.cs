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
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class EventRepository : Repository<CustomEvent, CustomEventDto>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(CustomEventDto eventToAdd)
        {
            eventToAdd.Key = eventToAdd.Year + eventToAdd.Country + eventToAdd.Name;
            foreach (var match in eventToAdd.Matches)
            {
                match.Key = eventToAdd.Key + match.Level + match.MatchNumber;
            }
            if (await ScoutAppContext.Events.AnyAsync(i => i.Key == eventToAdd.Key))
            {
                await Console.Error.WriteLineAsync("This event already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This event already exists!");
            }

            CustomEventDto clone = new CustomEventDto
            {
                Year = eventToAdd.Year,
                Country = eventToAdd.Country,
                Key = eventToAdd.Key,
                Name = eventToAdd.Name,
                StartDate = eventToAdd.StartDate
               
            };
            await base.Add(clone);
            await Context.SaveChangesAsync();
            var result = await ScoutAppContext.Events.FirstOrDefaultAsync(i =>
                i.Key == eventToAdd.Key);
            eventToAdd.Id = result.Id;
            return await Update(result, eventToAdd);
        }
        
        public async Task<ActionResult> Update(int id, CustomEventDto eventSource)
        {
            var result = await ScoutAppContext.Events.Include(i => i.Matches).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult("No event found to update!");
            }

            return await Update(result, eventSource);
        }

        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetAllByYear(int year)
        {
            var scoutForm = await ScoutAppContext.Events.Where(i => i.Year == year).ToListAsync();
            return Mapper.Map<List<CustomEventDto>>(scoutForm);
        }

        private async Task<ActionResult> Update(CustomEvent eventToUpdate, CustomEventDto eventSource)
        {
            var updated = Mapper.Map<ScoutForm>(eventSource);
            eventToUpdate = Mapper.Map(updated, eventToUpdate);
            Context.Update(eventToUpdate);
            return await Task.Run(() => new OkResult());
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
}
