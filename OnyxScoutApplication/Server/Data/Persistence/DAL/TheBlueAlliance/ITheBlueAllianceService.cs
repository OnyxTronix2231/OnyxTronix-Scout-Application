using System.Collections.Generic;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance
{
    public interface ITheBlueAllianceService
    {
        Task<List<Event>> GetEventsByYear(int year);
        Task<List<Match>> GetMatchesByEvent(string eventKey);
        Task<List<Match>> GetMatchesByTeamAndEvent(int teamNumber, string eventKey);
        Task<List<Team>> GetTeamsByEvent(string eventKey);
    }
}
