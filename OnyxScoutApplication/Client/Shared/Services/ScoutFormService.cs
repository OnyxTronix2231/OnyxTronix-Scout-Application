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
    private List<SimpleFormDto> adminGameScoutForms;

    public ScoutFormService(HttpClientManager httpClient, EventService eventService, AppManager appManager, ILocalStorageService localStorageService)
    {
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
        this.eventService = eventService;
    }
    
    public async Task OnInit(bool forceOnlineMode = false)
    {
        Console.WriteLine("initionalzingggg, online mode:" + appManager.IsOnlineMode);
        var selectedEvent = await eventService.GetSelectedEvent();
        var eventKey = selectedEvent.Key;
        var year = selectedEvent.Year;
        if (appManager.IsOnlineMode || forceOnlineMode)
        {
            var pitScoutFormsTask = httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.Pit}");
            var templateScoutFormTask = httpClient.GetJson<FormDto>($"ScoutFormFormat/TemplateScoutFormByYear/{year}");
            var templateScoutFormAdminTask = httpClient.GetJson<FormDto>($"ScoutFormFormat/TemplateScoutFormByYear/{year}/{ScoutFormType.Admin}", 
                showError: false); //We dont care if admin scout form template is missing here

            await Task.WhenAll(UpdateMainGameForms(), UpdateAdminForms(), pitScoutFormsTask, templateScoutFormTask, templateScoutFormAdminTask);

            pitScoutForms = await pitScoutFormsTask;
            pitScoutForms.Sort();
            await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.Pit.{eventKey}", pitScoutForms);

            var templateScoutForm = await templateScoutFormTask;
            await localStorageService.SetItemAsync($"ScoutFormService.TemplateScoutForm.{year}", templateScoutForm);
            
            var templateScoutFormAdmin = await templateScoutFormAdminTask;
            await localStorageService.SetItemAsync($"ScoutFormService.TemplateScoutForm.Admin.{year}", templateScoutFormAdmin);
            return;
        }
        
        mainGameScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.MainGame.{eventKey}");
        pitScoutForms = await localStorageService.GetItemAsync<List<SimpleFormDto>>($"ScoutFormService.ScoutForms.Pit.{eventKey}");
        // templateScoutForm = await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.{year}");
    }

    public async ValueTask<List<SimpleFormDto>> GetMainGameForms()
    {
        return await Task.FromResult(mainGameScoutForms);
    }
    
    public async ValueTask<List<SimpleFormDto>> GetAdminGameForms()
    {
        return await Task.FromResult(adminGameScoutForms);
    }
    
    public async ValueTask<List<SimpleFormDto>> GetPitForms()
    {
        return await Task.FromResult(pitScoutForms);
    }
    
    public async Task<FormDto> GetTemplateForm()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        return await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.{selectedEvent.Year}");
    }
    
    public async Task<FormDto> GetTemplateFormAdmin()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        return await localStorageService.GetItemAsync<FormDto>($"ScoutFormService.TemplateScoutForm.Admin.{selectedEvent.Year}");
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

    public async Task<bool> DeleteForm(string formId)
    {
        return await httpClient.TryDelete($"ScoutForm/{formId}");
    }

    public async Task UpdateMainGameForms()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        var eventKey = selectedEvent.Key;
        
        var mainGameScoutFormsTask = httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.MainGame}");
        mainGameScoutForms = await mainGameScoutFormsTask;
        mainGameScoutForms.Sort();
        await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.MainGame.{eventKey}", mainGameScoutForms);
    }
    
    public async Task UpdateAdminForms()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        var eventKey = selectedEvent.Key;
        
        var adminGameScoutFormsTask = httpClient.GetJson<List<SimpleFormDto>>($"ScoutForm/GetAllByEvent/{eventKey}/{ScoutFormType.Admin}");
        adminGameScoutForms = await adminGameScoutFormsTask;
        adminGameScoutForms.Sort();
        await localStorageService.SetItemAsync($"ScoutFormService.ScoutForms.MainGame.{eventKey}", adminGameScoutForms);
    }
}
