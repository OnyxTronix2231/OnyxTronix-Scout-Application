using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance
{
    public class TheBlueAllianceService : ITheBlueAllianceService
    {
        private const string PREFIX = "https://www.thebluealliance.com/api/v3";
        private readonly string key;

        public TheBlueAllianceService(string key)
        {
            this.key = key;
        }

        public async Task<List<string>> GetEventsKeysByTeamAndYear(int teamNumber, int year)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "team", "frc", teamNumber.ToString(), "events",
                year + "", "keys"));
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        public async Task<List<Event>> GetEventsByYear(int year)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "events", year.ToString(), "simple"));
            var result = JsonSerializer.Deserialize<List<Event>>(response,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return result;
        }

        public async Task<List<Match>> GetMatchesByEvent(string eventKey)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "event", eventKey, "matches", "simple"));
            var result = JsonSerializer.Deserialize<List<Match>>(response,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return result;
        }

        public async Task<List<Team>> GetTeamsByEvent(string eventKey)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "event", eventKey, "teams", "simple"));
            var result = JsonSerializer.Deserialize<List<Team>>(response,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return result;
        }

        public async Task<List<Team>> GetTeamsByDistrict(string districtKeyS)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "district", districtKeyS, "teams", "simple"));
            var result = JsonSerializer.Deserialize<List<Team>>(response,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return result;
        }

        public async Task<List<Match>> GetMatchesByTeamAndEvent(int teamNumber, string eventKey)
        {
            var response = await GetResponse(Path.Combine(PREFIX, "team", "frc" + teamNumber, "event",
                eventKey, "matches", "simple"));
            var result = JsonSerializer.Deserialize<List<Match>>(response,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return result;
        }

        private async Task<string> GetResponse(string request)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-TBA-Auth-Key", key);
            return await httpClient.GetStringAsync(request);
        }
    }
}
