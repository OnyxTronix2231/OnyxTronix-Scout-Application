using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Persistence.DAL.TheBlueAlliance;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;
using OnyxScoutApplication.Shared.Other.Analyzers;

namespace OnyxScoutApplication.Server.Controllers;

[OnyxAuthorize(Role = Role.Scouter)]
[ApiController]
[Route("[controller]")]
public class EventAnalyticsController : Controller
{
    private readonly IScoutFormUnitOfWork scoutFormUnitOfWork;
    private readonly IScoutFormFormatUnitOfWork scoutFormFormatUnitOfWork;
    private readonly ITheBlueAllianceService blueAllianceService;

    public EventAnalyticsController(IScoutFormUnitOfWork scoutFormUnitOfWork,
        IScoutFormFormatUnitOfWork scoutFormFormatUnitOfWork, ITheBlueAllianceService blueAllianceService)
    {
        this.scoutFormUnitOfWork = scoutFormUnitOfWork;
        this.scoutFormFormatUnitOfWork = scoutFormFormatUnitOfWork;
        this.blueAllianceService = blueAllianceService;
    }
    
    [HttpPost("GetEventAnalytics/{year:int}/{eventKey}")]
    public async Task<ActionResult<AnalyticsResult>> GetEventAnalytics(int year, string eventKey, AnalyticsSettings analyticsSettings)
    {
        var scoutFormsRes = await scoutFormUnitOfWork.ScoutForms.GetAllByEventWithData(eventKey, ScoutFormType.MainGame);
        var scoutForms = scoutFormsRes.Value!
            .Where(f => f.DateTime >= analyticsSettings.StartDate && f.DateTime <= analyticsSettings.EndDate).ToList();

        if (scoutFormsRes.Result is not null)
        {
            return scoutFormsRes.Result;
        }
        var scoutFormFormat = await scoutFormFormatUnitOfWork.ScoutFormFormats.GetWithFieldsByYear(year, ScoutFormType.MainGame);
        if (scoutFormFormat.Result is not null)
        {
            return scoutFormFormat.Result;
        }
        var teams = await blueAllianceService.GetTeamsByEvent(eventKey);
        TeamsAnalyzer analyzer = new TeamsAnalyzer(teams, scoutForms, scoutFormFormat.Value, analyticsSettings.EventAnalyticSettingsDto);
        return Ok(analyzer.Calc());
    }
    
    

}
