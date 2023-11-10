using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Client.Shared.Services;

public class TheBlueAllianceService : IService
{
    private readonly AppManager appManager;
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClientManager httpClient;
    private readonly EventService eventService;
    private List<Match> matches;
    private List<Event> events;
    private List<Team> teams;

    public TheBlueAllianceService(HttpClientManager httpClient, EventService eventService, AppManager appManager,
        ILocalStorageService localStorageService)
    {
        this.appManager = appManager;
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
        this.eventService = eventService;
    }


    public async Task OnInit()
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        var eventKey = selectedEvent.Key;
        var year = selectedEvent.Year;
        var country = selectedEvent.Country;

        if (appManager.IsOnlineMode)
        {
            matches = await httpClient.GetJson<List<Match>>($"TheBlueAlliance/GetAllMatches/{eventKey}");
            await localStorageService.SetItemAsync($"TheBlueAllianceService.Matches.{eventKey}", matches);

            events = await httpClient.GetJson<List<Event>>($"TheBlueAlliance/GetAllEvents/{year}");
            // if (events != null) //user is probably not authorized 
            // {
            events = events.Where(i => string.Equals(i.Country, country,
                StringComparison.OrdinalIgnoreCase)).OrderBy(i => i.StartDate).ToList();

            await localStorageService.SetItemAsync($"TheBlueAllianceService.Events.{year}", events);
            // }
            
            teams = await httpClient.GetJson<List<Team>>($"TheBlueAlliance/GetAllTeams/{eventKey}");
            await localStorageService.SetItemAsync($"TheBlueAllianceService.Teams.{eventKey}", teams);

            return;
        }

        matches = await localStorageService.GetItemAsync<List<Match>>($"TheBlueAllianceService.Matches.{eventKey}");
        events = await localStorageService.GetItemAsync<List<Event>>($"TheBlueAllianceService.Events.{year}");
        teams = await localStorageService.GetItemAsync<List<Team>>($"TheBlueAllianceService.Teams.{eventKey}");
    }
    
    public async Task<List<Match>> GetMatches()
    {
        return matches;
    }

    public async Task<List<Event>> GetEvents()
    {
        return events;
    }
    
    public async Task<List<Team>> GetTeams()
    {
        return teams;
    }

    public async Task<List<Match>> GetMatchesByTeamNumber(int teamNumber)
    {
        var selectedEvent = await eventService.GetSelectedEvent();
        if (appManager.IsOnlineMode)
        {
            return await httpClient.GetJson<List<Match>>($"TheBlueAlliance/GetMatchesByTeamAndEvent/" +
                                                         $"{teamNumber}/{selectedEvent.Key}");
        }

        return matches.Where(m => m.Alliances.IsInTeamBlue(teamNumber) || m.Alliances.IsInTeamRed(teamNumber)).ToList();
    }
}