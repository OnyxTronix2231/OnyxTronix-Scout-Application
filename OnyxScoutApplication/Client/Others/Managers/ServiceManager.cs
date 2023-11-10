using System;
using System.Threading.Tasks;
using OnyxScoutApplication.Client.Shared.Services;

namespace OnyxScoutApplication.Client.Others.Managers;

public class ServiceManager
{
    private readonly EventService eventService;
    private readonly TheBlueAllianceService theBlueAllianceService;
    private readonly ScoutFormFormatService formatService;
    private readonly ScoutFormService formService;

    public ServiceManager(EventService eventService, TheBlueAllianceService theBlueAllianceService,
        ScoutFormFormatService formatService, ScoutFormService formService)
    {
        this.eventService = eventService;
        this.theBlueAllianceService = theBlueAllianceService;
        this.formatService = formatService;
        this.formService = formService;
    }

    public async Task CleanAndInitAllServices()
    {
        await eventService.OnInit();
        if (eventService.GetSelectedEvent() is null)
        {
            Console.WriteLine("no event was selected yet!");
            return;
        }
        await theBlueAllianceService.OnInit();
        await formatService.OnInit();
        await formService.OnInit();
    }
}