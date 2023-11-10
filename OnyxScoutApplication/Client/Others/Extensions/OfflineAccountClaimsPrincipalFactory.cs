using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using OnyxScoutApplication.Client.Others.Managers;

namespace OnyxScoutApplication.Client.Others.Extensions;

public class OfflineAccountClaimsPrincipalFactory : RolesClaimsPrincipalFactory
{
    private readonly IServiceProvider services;

    public OfflineAccountClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor, IServiceProvider services) : base(accessor)
    {
        this.services = services;
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var localVehiclesStore = services.GetRequiredService<LocalUserManager>();

        var result = await base.CreateUserAsync(account, options);
        if (result.Identity.IsAuthenticated)
        {
            await localVehiclesStore.SaveUserAccountAsync(result);
        }
        else
        {
            result = await localVehiclesStore.LoadUserAccountAsync();
        }

        return result;
    }
}