using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Api;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class CustomEventRepository : FirestoreRepository<CustomEvent, CustomEventDto>, ICustomEventRepository
    {

        public CustomEventRepository(FirestoreDb  client, IMapper mapper) : base(client, mapper, "CustomEvents")
        {
        }

        public override async Task<ActionResult> Add(CustomEventDto eventDto)
        {
            eventDto.Key = eventDto.Year + eventDto.Country + eventDto.Name;
            var result = await CollectionReference.WhereEqualTo("Key", eventDto.Key).GetSnapshotAsync();
            if (result.Count != 0)
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This event already exists!");
            }

            UpdateMatchIds(eventDto);
            return await base.Add(eventDto);
            // CustomEventDto clone = new CustomEventDto
            // {
            //     Year = form.Year,
            //     Country = form.Country,
            //     Key = form.Key,
            //     Name = form.Name,
            //     StartDate = form.StartDate
            //    
            // };
            // await base.Add(clone);
            // await Context.SaveChangesAsync();
            // var result = await ScoutAppContext.Events.AsQueryable().FirstOrDefaultAsync(i =>
            //     i.Key == form.Key);
            // form.Id = result.Id;
            // return await Update(result, form);
        }
        
        public async Task<ActionResult> Update(string id, CustomEventDto eventSource)
        {
            UpdateMatchIds(eventSource);
            return await Update(eventSource);
        }

        public async Task<ActionResult<bool>> GetEventExists(string eventKey)
        {
            return (await CollectionReference.WhereEqualTo("Key", eventKey).GetSnapshotAsync()).Count != 0;
        }

        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetAllEventsByYear(int year)
        {
            var events = await CollectionReference.WhereEqualTo("Year", year).GetSnapshotAsync();
            return Mapper.Map<List<CustomEventDto>>(events.Select(i => i.ConvertTo<CustomEvent>()));
        }

        public async Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByEventKey(string eventKey)
        {
            var events = await CollectionReference.WhereEqualTo("Key", eventKey).GetSnapshotAsync();
            if (events.Count == 0)
            {
                return new BadRequestObjectResult($"No event found with key {eventKey} to update!");
            }

            return Mapper.Map<List<CustomMatchDto>>(events[0].ConvertTo<CustomEvent>().Matches);
        }

        public async Task<ActionResult<IEnumerable<CustomTeamDto>>> GetTeamsByEventKey(string eventKey)
        {
            var events = await CollectionReference.WhereEqualTo("Key", eventKey).GetSnapshotAsync();

            if (events.Count == 0)
            {
                return new BadRequestObjectResult($"No event found with key {eventKey}!");
            }

            var teams = events[0].ConvertTo<CustomEvent>().Matches.SelectMany(i => i.Alliances.Blue.Teams.Concat(i.Alliances.Red.Teams))
                .Distinct(new TeamsEqualityComparer());
            return Mapper.Map<List<CustomTeamDto>>(teams);
        }

        public async Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByTeamAndEventKey(int teamNumber,
            string eventKey)
        {
            var events = await CollectionReference.WhereEqualTo("Key", eventKey).GetSnapshotAsync();

            if (events.Count == 0)
            {
                return new BadRequestObjectResult($"No mathces found for team {teamNumber} and event {eventKey}!");
            }

            var resEvent = events[0].ConvertTo<CustomEvent>();
            resEvent.Matches = resEvent.Matches.Where(i => 
                i.Alliances.Blue.Teams.Any(t => t.TeamNumber == teamNumber) ||
                i.Alliances.Red.Teams.Any(t => t.TeamNumber == teamNumber)).ToList();
            
            return Mapper.Map<List<CustomMatchDto>>(resEvent.Matches);
        }

        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventWithMathesByKey(string eventKey)
        {
            var events = await CollectionReference.WhereEqualTo("Key", eventKey).GetSnapshotAsync();
            if (events.Count == 0)
            {
                return new BadRequestObjectResult($"No event found with key {eventKey} to update!");
            }

            return Mapper.Map<List<CustomEventDto>>(events[0].ConvertTo<CustomEvent>());
        }

        public async Task<ActionResult<CustomEventDto>> GetEventWithMatchesById(string id)
        {
            return await Get(id);
        }

        private async Task<ActionResult> Update(CustomEventDto eventSource)
        {
            DocumentReference docRef = CollectionReference.Document(eventSource.Id);
            foreach (var match in eventSource.Matches)
            {
                match.Date = DateTime.SpecifyKind(match.Date, DateTimeKind.Utc); 
            }
            await docRef.SetAsync(Mapper.Map<CustomEvent>(eventSource));
            return await Task.Run(() => new OkResult());
        }

        private static void UpdateMatchIds(CustomEventDto eventSource)
        {
            foreach (var customMatch in eventSource.Matches)
            {
                customMatch.Key = eventSource.Key + customMatch.Level + customMatch.MatchNumber;
            }

            // eventSource.Matches.ForEach(i =>
            // {
            //     i.Alliances.Blue.Teams.ForEach(ii => ii.CustomAllianceId = i.Alliances.Blue.Id);
            //     i.Alliances.Red.Teams.ForEach(ii => ii.CustomAllianceId = i.Alliances.Red.Id);
            // });
        }
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
