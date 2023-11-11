using System.Threading.Tasks;
using Blazored.LocalStorage;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Client.Shared.Services;

public class EventService : IService
{
    private readonly ILocalStorageService localStorageService;
    private Event selectedEvent;

    public EventService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }
    
    public async Task OnInit(bool forceOnlineMode = false)
    {
        selectedEvent = await localStorageService.GetItemAsync<Event>("EventService.SelectedEvent");
    }

    public async Task<Event> GetSelectedEvent()
    {
        return selectedEvent;
    }

    public async Task SetSelectedEvent(Event selectedEvent)
    {
        this.selectedEvent = selectedEvent;
        await localStorageService.SetItemAsync("EventService.SelectedEvent", selectedEvent);
    }
}