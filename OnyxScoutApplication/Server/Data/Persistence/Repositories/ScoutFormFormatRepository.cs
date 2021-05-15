﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormFormatRepository : Repository<ScoutFormFormat, ScoutFormFormatDto>, IScoutFormFormatRepository
    {
        public ScoutFormFormatRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ActionResult> Add(ScoutFormFormatDto scoutFormFormat)
        {
            if (await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormFormat.Year))
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest,
                    "This scout format already exists for this year!");
            }

           
            return await base.Add(scoutFormFormat);
        }

        public async Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year)
        {
            var result = await GetWithFieldsByYear(year);
            if (result.Value == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }

            return Mapper.Map<FormDto>(result.Value);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFields(int id)
        {
            var result = await ScoutAppContext.ScoutFormFormats.WithAllFields().FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found with the id of: " + id);
            }

            result.FieldsInStages = result.FieldsInStages.OrderBy(i => i.Index).ToList();
            result.FieldsInStages.ForEach(i => i.Fields.Sort((f1, f2) => f1.Index.CompareTo(f2.Index)));
            return Mapper.Map<ScoutFormFormatDto>(result);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year)
        {
            var result = await ScoutAppContext.ScoutFormFormats.WithAllFields().
                FirstOrDefaultAsync(i => i.Year == year);
            if (result == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }

            result.FieldsInStages = result.FieldsInStages.OrderBy(i => i.Index).ToList();
            var r = Mapper.Map<ScoutFormFormatDto>(result);
            return r;
        }

        public async Task<ActionResult> Update(int id, ScoutFormFormatDto scoutFormFormatDto)
        {
            if (!await ScoutAppContext.ScoutFormFormats.AnyAsync(i => i.Year == scoutFormFormatDto.Year))
            {
                return new BadRequestObjectResult("No scout from format found to update!");
            }

            var old = await ScoutAppContext.ScoutFormFormats.WithAllFields().FirstAsync(i => i.Year == scoutFormFormatDto.Year);
            var updated = Mapper.Map<ScoutFormFormat>(scoutFormFormatDto);
            updated = Mapper.Map(updated, old);
            Context.Update(updated);
            return await Task.Run(() => new OkResult());
        }

        private ApplicationDbContext ScoutAppContext => Context as ApplicationDbContext;
    }

    class FieldEquals : IEqualityComparer<Field>
    {
        public bool Equals(Field x, Field y)
        {
            if (x == y)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(Field obj)
        {
            return 0;
        }
    }
}
