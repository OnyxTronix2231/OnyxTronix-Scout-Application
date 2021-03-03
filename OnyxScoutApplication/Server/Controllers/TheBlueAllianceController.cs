using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TheBlueAllianceController : Controller
    {
        private readonly ITheBlueAllianceService theBlueAllianceService;

        public TheBlueAllianceController(ITheBlueAllianceService theBlueAllianceService)
        {
            this.theBlueAllianceService = theBlueAllianceService;
        }

        [HttpGet("GetAllEvents/{year}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByYear(int year)
        {
            return await theBlueAllianceService.GetEventsByYear(year);
        }

        [HttpGet("GetAllMatches/{eventkey}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByEventKey(string eventKey)
        {
            return await theBlueAllianceService.GetMatchesByEvent(eventKey);
        }

        [HttpGet("GetAllTeams/{eventkey}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsByEventKey(string eventKey)
        {
            return await theBlueAllianceService.GetTeamsByEvent(eventKey);
        }

        [HttpGet("GetMatchesByTeamAndEvent/{teamnumber}/{eventkey}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByTeamAndEventKey(int teamNumber, string eventKey)
        {
            return await theBlueAllianceService.GetMatchesByTeamAndEvent(teamNumber, eventKey);
        }
    }
}
