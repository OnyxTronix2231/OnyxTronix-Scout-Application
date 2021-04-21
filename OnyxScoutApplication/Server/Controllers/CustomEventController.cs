using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Controllers
{
    [OnyxAuthorize(Role = Role.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class CustomEventController
    {
        private readonly ICustomEventUnitOfWork unitOfWork;

        public CustomEventController(ICustomEventUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateCustomEvent([FromBody] CustomEventDto customEvent)
        {
            var response = await unitOfWork.CustomEvents.Add(customEvent);
            await unitOfWork.Complete();
            return response;

        }

        [HttpGet("GetAllEventsByYear/{year}")]
        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventsByYear(int year)
        {
            return await unitOfWork.CustomEvents.GetAllByYear(year);
        }

        [HttpGet("GetEventByKey/{key}")]
        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventsByYearKey(string key)
        {
            return await unitOfWork.CustomEvents.GetEventByKey(key);
        }
    }
}
