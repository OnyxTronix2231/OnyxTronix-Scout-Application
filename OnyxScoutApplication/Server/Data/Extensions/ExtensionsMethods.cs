﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Extensions
{
    public static class ExtensionsMethods
    {
        public static IQueryable<CustomEvent> WithAllMatches(this IQueryable<CustomEvent> queryable)
        {
            return queryable.Include(i => i.Matches).ThenInclude(i => i.Alliances).ThenInclude(i => i.Blue)
                .ThenInclude(i => i.Teams)
                .Include(i => i.Matches).ThenInclude(i => i.Alliances).ThenInclude(i => i.Red)
                .ThenInclude(i => i.Teams);
        }
        

        public static IQueryable<ScoutFormFormat>
            WithAllFields(this IQueryable<ScoutFormFormat> queryable)
        {
            return queryable.Include(i => i.FieldsInStages)
                .ThenInclude(f => f.Fields).ThenInclude(i => i.Options)
                .Include(i => i.FieldsInStages)
                .ThenInclude(f => f.Fields)
                .ThenInclude(i => i.CascadeFields).ThenInclude(i => i.Options)
                .Include(i => i.FieldsInStages)
                .ThenInclude(f => f.Fields)
                .ThenInclude(i => i.CascadeFields).ThenInclude(i => i.CascadeFields).ThenInclude(i => i.Options)
                .Include(i => i.FieldsInStages)
                .ThenInclude(f => f.Fields)
                .ThenInclude(i => i.CascadeFields).ThenInclude(i => i.CascadeFields)
                .ThenInclude(i => i.CascadeFields).ThenInclude(i => i.Options)
                //.AsSplitQuery()
                ;
        }
    }
}
