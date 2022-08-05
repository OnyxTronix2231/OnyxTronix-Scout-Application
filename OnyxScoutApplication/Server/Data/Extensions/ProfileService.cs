using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace OnyxScoutApplication.Server.Data.Extensions
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var nameClaim = context.Subject.FindAll(JwtClaimTypes.Name);
            context.IssuedClaims.AddRange(nameClaim);

            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            context.IssuedClaims.AddRange(roleClaims);

            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await Task.CompletedTask;
        }
    }
}
