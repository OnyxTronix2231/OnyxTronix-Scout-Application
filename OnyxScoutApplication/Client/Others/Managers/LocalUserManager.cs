using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace OnyxScoutApplication.Client.Others.Managers;

public class LocalUserManager
{
    private readonly ILocalStorageService localStorageService;

    public LocalUserManager(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async ValueTask SaveUserAccountAsync(ClaimsPrincipal user)
    {
        // user != null
        if (user is not null)
        {
            await localStorageService.SetItemAsync("metadata.userAccount",
                user.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }));
            return;
        }
        await localStorageService.RemoveItemAsync("metadata.userAccount");
    }

    public async Task<ClaimsPrincipal> LoadUserAccountAsync()
    {
        var storedClaims = await localStorageService.GetItemAsync<ClaimData[]>("metadata.userAccount");
        Console.WriteLine(ClaimTypes.Name);
        return storedClaims != null
            ? new ClaimsPrincipal(new ClaimsIdentity(storedClaims.Select(c => new Claim(c.Type, c.Value)), "appAuth"))
            : new ClaimsPrincipal(new ClaimsIdentity());
    }
    
    class ClaimData
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}