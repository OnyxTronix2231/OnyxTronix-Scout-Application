using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;

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
    }
}
