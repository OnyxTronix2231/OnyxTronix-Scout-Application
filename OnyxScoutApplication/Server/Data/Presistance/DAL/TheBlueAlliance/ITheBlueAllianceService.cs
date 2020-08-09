using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Shared.Data.Presistance.TheBlueAlliance
{
    public interface ITheBlueAllianceService
    {
        Task<List<Event>> GetEventsByYear(int year);
        Task<List<string>> GetEventsKeysByTeamAndYear(int teamNumber, int year);
        Task<List<Match>> GetMatchesByEvent(string eventKey);
        Task<List<Match>> GetMatchesByTeamAndEvent(int teamNumber, string eventKey);
        Task<List<string>> GetMatchesKeysByTeamAndEvent(int teamNumber, string evenmName);
        Task<List<Team>> GetTeamsByEvent(string eventKey);
    }
}
