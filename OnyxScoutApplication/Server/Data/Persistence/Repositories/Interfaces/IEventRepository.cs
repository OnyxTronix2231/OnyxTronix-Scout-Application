﻿using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IEventRepository : IRepository<CustomEventDto>
    {
        Task<ActionResult> Update(int id, CustomEventDto eventSource);

        Task<ActionResult<IEnumerable<CustomEventDto>>> GetAllByYear(int year);

        Task<ActionResult<IEnumerable<CustomMatch>>> GetMatchesByEventKey(string eventKey);

        Task<ActionResult<IEnumerable<Team>>> GetTeamsByEventKey(string eventKey);
        
        Task<ActionResult<IEnumerable<CustomMatch>>> GetMatchesByTeamAndEventKey(int teamNumber, string eventKey);
    }
}
