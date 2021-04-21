using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface ICustomEventRepository : IRepository<CustomEventDto>
    {
        Task<ActionResult> Update(int id, CustomEventDto eventSource);
        
        Task<ActionResult<bool>> GetEventExists(string eventKey);

        Task<ActionResult<IEnumerable<CustomEventDto>>> GetAllEventsByYear(int year);

        Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByEventKey(string eventKey);

        Task<ActionResult<IEnumerable<CustomTeamDto>>> GetTeamsByEventKey(string eventKey);
        
        Task<ActionResult<IEnumerable<CustomMatchDto>>> GetMatchesByTeamAndEventKey(int teamNumber, string eventKey);
        
        Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventWithMathesByKey(string key);
        
        Task<ActionResult<CustomEventDto>> GetEventWithMatchesById(int id);
    }
}
