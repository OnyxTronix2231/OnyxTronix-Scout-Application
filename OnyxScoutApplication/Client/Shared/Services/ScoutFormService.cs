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
    private readonly EventService eventService;
    private List<SimpleFormDto> mainGameScoutForms;
    private List<SimpleFormDto> pitScoutForms;

    public ScoutFormService(HttpClientManager httpClient, EventService eventService, AppManager appManager, ILocalStorageService localStorageService)
    {
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
        this.eventService = eventService;
    }
    
    public async Task OnInit()
    {
        Console.WriteLine("initionalzingggg, online mode:" + appManager.IsOnlineMode);
        var selectedEvent = await eventService.GetSelectedEvent();
        var eventKey = selectedEvent.Key;
        var year = selectedEvent.Year;
        if (appManager.IsOnlineMode)
        {
            mainGameScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.MainGame}");
            mainGameScoutForms.Sort();
            await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.MainGame.{eventKey}", mainGameScoutForms);
            
            pitScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.Pit}");
            pitScoutForms.Sort();
            await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.Pit.{eventKey}", pitScoutForms);
            
            var templateScoutForm = await httpClient.GetJson<FormDto>($"ScoutFormFormat/TemplateScoutFormByYear/{year}"); 
            await localStorageService.SetItemAsync($"ScoutFormService.TemplateScoutForm.{year}", templateScoutForm);
            return;
        }
        
        mainGameScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.MainGame.{eventKey}");
        pitScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.Pit.{eventKey}");
        // templateScoutForm = await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.{year}");
    }

    public async Task<List<SimpleFormDto>> GetMainGameForms()
    {
        return mainGameScoutForms;
    }
    
    public async Task<List<SimpleFormDto>> GetPitForms()
    {
        return pitScoutForms;
    }
    
    public async Task<FormDto> GetTemplateForm()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        return await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.{selectedEvent.Year}");;
    }


    public async Task<List<FormDto>> GetPitFormsByTeamNumber(int teamNumber)
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        if (appManager.IsOnlineMode)
        {
            return await httpClient.GetJson<List<FormDto>>($"ScoutForm/GetAllByTeam/{teamNumber}/{selectedEvent.Key}/{ScoutFormType.Pit}");
        }
        
        return new List<FormDto>(); //Not supported in offline mode
    }
}