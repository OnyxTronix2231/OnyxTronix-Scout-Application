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
    private FormDto templateScoutForm;

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
        var eventKey = eventService.GetSelectedEvent().Key;
        var year = eventService.GetSelectedEvent().Year;
        if (appManager.IsOnlineMode)
        {
            mainGameScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.MainGame}");
            mainGameScoutForms.Sort();
            await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.MainGame.{eventKey}", mainGameScoutForms);
            
            pitScoutForms = await httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.Pit}");
            pitScoutForms.Sort();
            await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.Pit.{eventKey}", pitScoutForms);
            
            templateScoutForm = await httpClient.GetJson<FormDto>($"ScoutFormFormat/TemplateScoutFormByYear/{year}"); 
            await localStorageService.SetItemAsync($"ScoutFormService.TemplateScoutForm.{year}", templateScoutForm);
            return;
        }
        
        mainGameScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.MainGame.{eventKey}");
        pitScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.Pit.{eventKey}");
        templateScoutForm = await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.{year}");
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
        return templateScoutForm;
    }
    
    
    
}