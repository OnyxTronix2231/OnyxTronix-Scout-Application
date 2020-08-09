using OnyxScoutApplication.Shared.Data.Presistance.TheBlueAlliance;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.TheBlueAlliance
{
    public class TheBlueAllianceService : ITheBlueAllianceService
    {
        private readonly string prefix = "https://www.thebluealliance.com/api/v3";
        public string key;
        public TheBlueAllianceService(string key)
        {
            this.key = key;
        }

        public async Task<List<string>> GetEventsKeysByTeamAndYear(int teamNumber, int year)
        {
            var response = await GetResponse(Path.Combine(prefix, "team" , "frc", teamNumber.ToString(), "events", year + "", "keys"));
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        //www.thebluealliance.com/api/v3/team/frc2231/event/2017isde3/matches/keys
        public async Task<List<string>> GetMatchesKeysByTeamAndEvent(int teamNumber, string evenmName)
        {
            var response = await GetResponse(Path.Combine(prefix, "team", "frc", teamNumber.ToString(), "event", evenmName, "matches", "keys"));
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        public async Task<List<Event>> GetEventsByYear(int year)
        {
            var response = await GetResponse(Path.Combine(prefix, "events", year.ToString(), "simple"));
            var result = JsonSerializer.Deserialize<List<Event>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<Match>> GetMatchesByEvent(string eventKey)
        {
            var response = await GetResponse(Path.Combine(prefix, "event", eventKey, "matches", "simple"));
            var result = JsonSerializer.Deserialize<List<Match>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<Team>> GetTeamsByEvent(string eventKey)
        {
            var response = await GetResponse(Path.Combine(prefix, "event", eventKey, "teams", "simple"));
            var result = JsonSerializer.Deserialize<List<Team>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        public async Task<List<Match>> GetMatchesByTeamAndEvent(int teamNumber, string eventKey)
        {
            var response = await GetResponse(Path.Combine(prefix, "team", "frc", teamNumber.ToString(), "event", eventKey, "matches", "simple"));
            var result = JsonSerializer.Deserialize<List<Match>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }

        private async Task<string> GetResponse(string request)
        {
            string response;
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-TBA-Auth-Key", key);
            return response = await httpClient.GetStringAsync(request);
        }
    }
}
