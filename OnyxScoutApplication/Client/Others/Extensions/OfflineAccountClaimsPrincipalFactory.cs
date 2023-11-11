using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            Console.WriteLine("User is AUTHENTICATED saving to cache");
            await localVehiclesStore.SaveUserAccountAsync(result);
        }
        else
        {
            Console.WriteLine("USER IS NOT AUTHENTICATED loading from cache");
            result = await localVehiclesStore.LoadUserAccountAsync();
        }

        return result;
    }
}