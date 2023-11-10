using System;
using System.Threading.Tasks;
using OnyxScoutApplication.Client.Shared.Services;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Client.Others.Managers;

public class ServiceManager
{
    private readonly EventService eventService;
    private readonly TheBlueAllianceService theBlueAllianceService;
    private readonly ScoutFormFormatService formatService;
    private readonly ScoutFormService formService;
    private readonly HttpClientManager httpClientManager;
    private readonly AppManager appManager;

    public ServiceManager(EventService eventService, TheBlueAllianceService theBlueAllianceService,
        ScoutFormFormatService formatService, ScoutFormService formService, HttpClientManager httpClientManager,
        AppManager appManager)
    {
        this.eventService = eventService;
        this.theBlueAllianceService = theBlueAllianceService;
        this.formatService = formatService;
        this.formService = formService;
        this.httpClientManager = httpClientManager;
        this.appManager = appManager;
    }

    public async Task CleanAndInitAllServices()
    {
        if (await httpClientManager.GetJson<ServerStatus>("ScoutForm/ServerStatus") is null)
        {
            appManager.IsOnlineMode = false;
        }
        
        await eventService.OnInit();
        if (await eventService.GetSelectedEvent() is null)
        {
            Console.WriteLine("no event was selected yet!");
            return;
        }
        await theBlueAllianceService.OnInit();
        await formatService.OnInit();
        await formService.OnInit();
    }
}