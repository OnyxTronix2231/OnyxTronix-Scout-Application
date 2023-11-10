using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Client.Shared.Services;

public class ScoutFormService: IService
{
    private readonly AppManager appManager;
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClientManager httpClient;
    private List<SimpleFormDto> mainGameScoutForms;
    private List<SimpleFormDto> pitScoutForms;

    public ScoutFormService(HttpClientManager httpClient, AppManager appManager, ILocalStorageService localStorageService)
    {
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
    }
    
    public async Task OnInit(string eventKey)
    {
        Console.WriteLine("initionalzingggg");
        if (appManager.IsOnlineMode)
        {
            mainGameScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.MainGame}");
            await localStorageService.SetItemAsync($"ScoutForms.MainGame.{eventKey}", mainGameScoutForms);
            
            pitScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.Pit}");
            await localStorageService.SetItemAsync($"ScoutForms.Pit.{eventKey}", pitScoutForms);
            return;
        }
        
        mainGameScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutForms.MainGame.{eventKey}");
        pitScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutForms.Pit.{eventKey}");
    }

    public async Task<List<SimpleFormDto>> GetMainGameForms()
    {
        return mainGameScoutForms;
    }
    
    public async Task<List<SimpleFormDto>> GetPitForms()
    {
        return pitScoutForms;
    }
    
    
    
}