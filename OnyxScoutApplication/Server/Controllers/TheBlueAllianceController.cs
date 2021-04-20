using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TheBlueAllianceController : Controller
    {
        private readonly ITheBlueAllianceService theBlueAllianceService;
        private readonly ICustomEventRepository customEventRepository;
        private readonly IMapper mapper;

        public TheBlueAllianceController(ITheBlueAllianceService theBlueAllianceService, 
            ICustomEventRepository customEventRepository, IMapper mapper)
        {
            this.theBlueAllianceService = theBlueAllianceService;
            this.customEventRepository = customEventRepository;
            this.mapper = mapper;
        }

        [HttpGet("GetAllEvents/{year}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByYear(int year)
        {
            var customEvents = await customEventRepository.GetAllByYear(year);
            var tbaEvents = await theBlueAllianceService.GetEventsByYear(year);
            var mappedCustomEvents = mapper.Map<IEnumerable<Event>>(customEvents);
            return tbaEvents.Concat(mappedCustomEvents).ToList();
        }

        [HttpGet("GetAllMatches/{eventKey}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByEventKey(string eventKey)
        {
            var results = await theBlueAllianceService.GetMatchesByEvent(eventKey);
            if (results != null && results.Any())
            {
                return results;
            }
            
            var customMatches = await customEventRepository.GetMatchesByEventKey(eventKey);
            results = mapper.Map<List<Match>>(customMatches);
            return results;
        }

        [HttpGet("GetAllTeams/{eventKey}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsByEventKey(string eventKey)
        {
            var results = await theBlueAllianceService.GetTeamsByEvent(eventKey);
            if (results != null && results.Any())
            {
                return results;
            }
            
            var customMatches = await customEventRepository.GetTeamsByEventKey(eventKey);
            results = mapper.Map<List<Team>>(customMatches);
            return results;
        }

        [HttpGet("GetMatchesByTeamAndEvent/{teamNumber}/{eventKey}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchesByTeamAndEventKey(int teamNumber, string eventKey)
        {
            var results = await theBlueAllianceService.GetMatchesByTeamAndEvent(teamNumber, eventKey);
            if (results != null && results.Any())
            {
                return results;
            }
            
            var customMatches = await customEventRepository.GetMatchesByTeamAndEventKey(teamNumber, eventKey);
            results = mapper.Map<List<Match>>(customMatches);
            return results;
        }
    }
}
