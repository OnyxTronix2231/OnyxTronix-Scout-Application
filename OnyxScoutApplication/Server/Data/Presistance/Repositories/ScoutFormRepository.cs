using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories
{
    public class ScoutFormRepository : Repository<ScoutForm, ScoutFormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormDto scoutForm)
        {
            if (await ScoutAppContext.ScoutForms.AnyAsync(i => i.Year == scoutForm.Year && i.MatchName == scoutForm.MatchName && i.TeamNumber == scoutForm.TeamNumber))
            {
                Console.WriteLine("This scout form already exists!");
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }

            ScoutFormDto clone = new ScoutFormDto()
            {
                Year = scoutForm.Year,
                TeamNumber = scoutForm.TeamNumber,
                MatchName = scoutForm.MatchName,
                WriterUserName = scoutForm.WriterUserName
            };
            await base.Add(clone);
            await context.SaveChangesAsync();
            var result = await ScoutAppContext.ScoutForms.FirstOrDefaultAsync(i => i.Year == scoutForm.Year && i.MatchName == scoutForm.MatchName && i.TeamNumber == scoutForm.TeamNumber);
            scoutForm.Id = result.Id;
            return await Update(result, scoutForm);
        }

        public async Task<ActionResult<ScoutFormDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Id == id);
            if(result == null)
            {
                return new NotFoundObjectResult("No scout form found with the id of: " + id);
            }
            return mapper.Map<ScoutFormDto>(result);
        }

        public async Task<ActionResult<ScoutFormDto>> GetWithDataByYear(int year)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form found for year - " + year);
            }
            return mapper.Map<ScoutFormDto>(result);
        }

        public async Task<ActionResult> Update(int id, ScoutFormDto scoutFormDto)
        {
            var result = await ScoutAppContext.ScoutForms.Include(i => i.Data).FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new BadRequestObjectResult("No scout from found to update!");
            }
            return await Update(result, scoutFormDto);
        }
        
        public async Task<ActionResult> Update(ScoutForm scoutForm, ScoutFormDto scoutFormDto)
        {
            var updated = mapper.Map<ScoutForm>(scoutFormDto);
            scoutForm = mapper.Map(updated, scoutForm);
            RecursivelySetScoutFormId(scoutForm.Id, scoutForm.Data);
            context.Update(scoutForm);
            return new OkResult();
        }

        private void RecursivelySetScoutFormId(int id, List<ScoutFormData> data)
        {
            foreach (ScoutFormData aData in data)
            {
                aData.ScoutFormId = id;
                RecursivelySetScoutFormId(id, aData.CascadeData);
            }
        }

        public async Task<ActionResult<IEnumerable<ScoutFormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey)
        {
            var scoutForm = await ScoutAppContext.ScoutForms.Include(i => i.Data).ThenInclude(sn => sn.Field).Where(i => i.TeamNumber == teamNumber && i.MatchName.Contains(eventKey)).ToListAsync();
            return mapper.Map<List<ScoutFormDto>>(scoutForm);
        }

        private ApplicationDbContext ScoutAppContext
        {
            get
            {
                return context as ApplicationDbContext;
            }
        }
    }
}
