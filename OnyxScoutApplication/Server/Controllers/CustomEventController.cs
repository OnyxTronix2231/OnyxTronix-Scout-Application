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
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomEvent(int id, [FromBody] CustomEventDto scoutFormModel)
        {
            var response = await unitOfWork.CustomEvents.Update(id, scoutFormModel);
            await unitOfWork.Complete();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomEventDto>> GetById(int id)
        {
            return await unitOfWork.CustomEvents.GetEventWithMatchesById(id);
        }
        
        [HttpGet("GetEventByKey/{key}")]
        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventByKey(string key)
        {
            return await unitOfWork.CustomEvents.GetEventWithMathesByKey(key);
        }

        [HttpGet("GetAllEventsByYear/{year}")]
        public async Task<ActionResult<IEnumerable<CustomEventDto>>> GetEventsByYear(int year)
        {
            return await unitOfWork.CustomEvents.GetAllEventsByYear(year);
        }
    }
}
