using System.Collections.Generic;
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
        public static IIncludableQueryable<CustomEvent, List<CustomTeam>> WithAllMatches(this IQueryable<CustomEvent> queryable)
        {
            return queryable.Include(i => i.Matches).ThenInclude(i => i.Alliances).ThenInclude(i => i.Blue)
                .ThenInclude(i => i.Teams)
                .Include(i => i.Matches).ThenInclude(i => i.Alliances).ThenInclude(i => i.Red)
                .ThenInclude(i => i.Teams);
        }
        
        public static IIncludableQueryable<Form, Field> WithAllFields(this IQueryable<Form> queryable)
        {
            return queryable.Include(i => i.FormDataInStages).ThenInclude(i => i.FormData).ThenInclude(i => i.Field)

                .Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.CascadeData).ThenInclude(i => i.Field)

                .Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.CascadeData).ThenInclude(i => i.CascadeData).ThenInclude(i => i.Field)

                .Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.CascadeData).ThenInclude(i => i.CascadeData).ThenInclude(i => i.CascadeData)
                .ThenInclude(i => i.Field)

                .Include(i => i.FormDataInStages).ThenInclude(i => i.FormData)
                .ThenInclude(i => i.Field);
        }
    }
}
