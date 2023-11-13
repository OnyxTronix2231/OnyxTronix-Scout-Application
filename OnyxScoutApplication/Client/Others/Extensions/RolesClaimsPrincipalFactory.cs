using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace OnyxScoutApplication.Client.Others.Extensions
{
    public class RolesClaimsPrincipalFactory : OfflineAccountClaimsPrincipalFactory
    {
        public RolesClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor, IServiceProvider services)
            : base(accessor, services)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);
            
            if (!user.Identity.IsAuthenticated)
                return user;
            
            var identity = (ClaimsIdentity)user.Identity;
            var roleClaims = identity.FindAll(identity.RoleClaimType);

            var existingRoleClaims = roleClaims as Claim[] ?? roleClaims.ToArray();
            
            if (!existingRoleClaims.Any()) 
                return user;
            
            foreach (var existingClaim in existingRoleClaims)
            {
                identity.RemoveClaim(existingClaim);
            }

            var rolesElem = account.AdditionalProperties[identity.RoleClaimType];
            
            if (rolesElem is not JsonElement roles) return user;
            
            if (roles.ValueKind == JsonValueKind.Array)
            {
                foreach (var role in roles.EnumerateArray())
                {
                    identity.AddClaim(new Claim(options.RoleClaim, role.GetString()));
                }
            }
            else
            {
                identity.AddClaim(new Claim(options.RoleClaim, roles.GetString()));
            }

            return user;
        }
    }
}
