using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Shared.Services;

public class ScoutFormFormatService: IService
{
    private readonly AppManager appManager;
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClientManager httpClient;
    private readonly EventService eventService;
    private ScoutFormFormatDto mainGameScoutFormFormat;
    private ScoutFormFormatDto pitScoutFormFormat;

    public ScoutFormFormatService(HttpClientManager httpClient, EventService eventService, AppManager appManager, ILocalStorageService localStorageService)
    {
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
        this.eventService = eventService;
    }
    
    public async Task OnInit(bool forceOnlineMode = false)
    {
        var year = (await eventService.GetSelectedEvent()).Year;

        if (appManager.IsOnlineMode || forceOnlineMode)
        {
            mainGameScoutFormFormat = await httpClient.GetJson<ScoutFormFormatDto>($"ScoutFormFormat/ByYear/{year}/{ScoutFormType.MainGame}");
            await localStorageService.SetItemAsync($"ScoutFormFormatService.MainGame.{year}", mainGameScoutFormFormat);
            
            pitScoutFormFormat = await httpClient.GetJson<ScoutFormFormatDto>($"ScoutFormFormat/ByYear/{year}/{ScoutFormType.Pit}");
            await localStorageService.SetItemAsync($"ScoutFormFormatService.Pit.{year}", pitScoutFormFormat);
            return;
        }
        
        mainGameScoutFormFormat = await localStorageService.GetItemAsync<ScoutFormFormatDto>($"ScoutFormFormatService.MainGame.{year}");
        pitScoutFormFormat = await localStorageService.GetItemAsync<ScoutFormFormatDto>($"ScoutFormFormatService.Pit.{year}");
    }

    public async Task<ScoutFormFormatDto> GetMainGameScoutFormFormat()
    {
        return mainGameScoutFormFormat;
    }
    
    public async Task<ScoutFormFormatDto> GetPitScoutFormFormat()
    {
        return pitScoutFormFormat;
    }

    public async Task<ScoutFormFormatDto> GetScoutFormById(string id)
    {
        return await httpClient.GetJson<ScoutFormFormatDto>($"ScoutFormFormat/{id}");
    }
    
}