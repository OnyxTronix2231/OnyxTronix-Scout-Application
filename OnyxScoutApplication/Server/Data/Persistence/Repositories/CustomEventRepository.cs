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
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class CustomEventRepository : Repository<CustomEvent, CustomEventDto>, ICustomEventRepository
    {
        private readonly ITheBlueAllianceService allianceService;

        public CustomEventRepository(ApplicationDbContext context, ITheBlueAllianceService allianceService,
            IMapper mapper) : base(context, mapper)
        {
            this.allianceService = allianceService;
        }

        public override async Task<ActionResult> Add(CustomEventDto eventToAdd)
        {
            eventToAdd.Key = eventToAdd.Year + eventToAdd.Country + eventToAdd.Name;
            foreach (var customMatch in eventToAdd.Matches)
            {
                customMatch.Key = eventToAdd.Key + customMatch.Level + customMatch.MatchNumber;
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
            eventSource.Matches.ForEach(i =>
            {
                i.Alliances.Blue.Teams.ForEach(ii => ii.CustomAllianceId = i.Alliances.Blue.Id);
                i.Alliances.Red.Teams.ForEach(ii => ii.CustomAllianceId = i.Alliances.Red.Id);
            });
            return await Update(eventSource);
        }

        public async Task<ActionResult<bool>> GetEventExists(string eventKey)
        {
            return await ScoutAppContext.Events.AnyAsync(i => i.Key == eventKey);
        }

        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetAllEventsByYear(int year)
        {
            var events = await ScoutAppContext.Events.Where(i => i.Year == year).ToListAsync();
            return Mapper.Map<List<CustomEventDto>>(events);
        }

        public async Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByEventKey(string eventKey)
        {
            var result = await ScoutAppContext.Events.WithAllMatches().FirstOrDefaultAsync(i => i.Key == eventKey);
            if (result == null)
            {
                return new BadRequestObjectResult($"No event found with key {eventKey} to update!");
            }

            return Mapper.Map<List<CustomMatchDto>>(result.Matches);
        }

        public async Task<ActionResult<IEnumerable<CustomTeamDto>>> GetTeamsByEventKey(string eventKey)
        {
            var result = await ScoutAppContext.Events.WithAllMatches().FirstOrDefaultAsync(i => i.Key == eventKey);
           
            if (result == null)
            {
                return new BadRequestObjectResult($"No event found with key {eventKey}!");
            }

            var teams = result.Matches.SelectMany(i => i.Alliances.Blue.Teams.Concat(i.Alliances.Red.Teams))
                .Distinct(new TeamsEqualityComparer() );
            return Mapper.Map<List<CustomTeamDto>>(teams);
        }

        public async Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByTeamAndEventKey(int teamNumber,
            string eventKey)
        {
            var result = await ScoutAppContext.Events.WithAllMatches().FirstOrDefaultAsync(i => i.Key == eventKey);

            if (result == null)
            {
                return new BadRequestObjectResult($"No mathces found for team {teamNumber} and event {eventKey}!");
            }

            result.Matches = result.Matches.Where(i => i.Alliances.Blue.Teams.Any(t => t.TeamNumber == teamNumber) ||
                                                       i.Alliances.Red.Teams.Any(t => t.TeamNumber == teamNumber))
                .ToList();
            return Mapper.Map<List<CustomMatchDto>>(result.Matches);
        }

        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventWithMathesByKey(string key)
        {
            var result = await ScoutAppContext.Events.WithAllMatches().FirstOrDefaultAsync(i => i.Key == key);
            if (result == null)
            {
                return new BadRequestObjectResult($"No event found with key {key} to update!");
            }

            return Mapper.Map<List<CustomEventDto>>(result);
        }

        public async Task<ActionResult<CustomEventDto>> GetEventWithMatchesById(int id)
        {
            var result = await ScoutAppContext.Events.WithAllMatches().FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult($"No event found with id {id}!");
            }

            return Mapper.Map<CustomEventDto>(result);
        }

        private async Task<ActionResult> Update(CustomEvent eventToUpdate, CustomEventDto eventSource)
        {
            var updated = Mapper.Map<CustomEvent>(eventSource);
            eventToUpdate = Mapper.Map(updated, eventToUpdate);
            Context.Update(eventToUpdate);
            return await Task.Run(() => new OkResult());
        }
        
        private async Task<ActionResult> Update(CustomEventDto eventSource)
        {
            Context.Update(Mapper.Map<CustomEvent>(eventSource));
            return await Task.Run(() => new OkResult());
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }
    

    public class TeamsEqualityComparer : IEqualityComparer<CustomTeam>
    {
        public bool Equals(CustomTeam x, CustomTeam y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.TeamNumber == y.TeamNumber;
        }

        public int GetHashCode(CustomTeam obj)
        {
            return HashCode.Combine(obj.TeamNumber);
        }
    }
}
