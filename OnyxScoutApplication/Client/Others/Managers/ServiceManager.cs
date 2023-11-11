using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
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
    private readonly ILocalStorageService localStorageService;
    private readonly NotificationManager notificationManager;

    public ServiceManager(EventService eventService, TheBlueAllianceService theBlueAllianceService,
        ScoutFormFormatService formatService, ScoutFormService formService, HttpClientManager httpClientManager,
        AppManager appManager, ILocalStorageService localStorageService, NotificationManager notificationManager)
    {
        this.eventService = eventService;
        this.theBlueAllianceService = theBlueAllianceService;
        this.formatService = formatService;
        this.formService = formService;
        this.httpClientManager = httpClientManager;
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.notificationManager = notificationManager;
    }

    public async Task<bool> CleanAndInitAllServices(bool forceOnlineMode = false)
    {
        if (await httpClientManager.GetJson<ServerStatus>("ScoutForm/ServerStatus") is null)
        {
            appManager.IsOnlineMode = false;
            if (forceOnlineMode)
            {
                return false;
            }
        }


        await eventService.OnInit(forceOnlineMode);
        var selectedEvent = await eventService.GetSelectedEvent();
        if (selectedEvent is null)
        {
            Console.WriteLine("no event was selected yet!");
            return false;
        }

        if (!appManager.IsOnlineMode && !forceOnlineMode)
        {
            if (!await localStorageService.GetItemAsync<bool>($"ServiceManager.CachedEvent.{selectedEvent.Key}"))
            {
                await notificationManager.NotifyAsync("Error",
                    $"No cache was found for the selected event{selectedEvent.Name}.\n" +
                    $"Please switch to online mode", NotificationType.Danger);
                return false;
            }
        }

        await Task.WhenAll(
            theBlueAllianceService.OnInit(forceOnlineMode),
            formatService.OnInit(forceOnlineMode),
            formService.OnInit(forceOnlineMode));
        
        await localStorageService.SetItemAsync($"ServiceManager.CachedEvent.{selectedEvent.Key}", true);
        return true;
    }
}