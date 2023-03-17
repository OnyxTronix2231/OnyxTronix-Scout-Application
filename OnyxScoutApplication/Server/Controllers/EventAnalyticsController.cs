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
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

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
    
    [HttpPost("GetTeamEventAnalytics/{year:int}/{eventKey}/{teamNumber:int}")]
    public async Task<ActionResult<AnalyticsResult>> GetTeamEventAnalytics(int year, string eventKey, int teamNumber, AnalyticsSettings analyticsSettings)
    {
        Dictionary<string, List<TeamFieldAverage>> calculatedScoutDataByStages =
            new Dictionary<string, List<TeamFieldAverage>>();
        
        Dictionary<string, List<TeamFieldAverage>> calculatedScoutDataByStagesPit =
            new Dictionary<string, List<TeamFieldAverage>>();
        
        
        var scoutFormsRes = await scoutFormUnitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey, ScoutFormType.MainGame);
        if (scoutFormsRes.Result is not null)
        {
            return scoutFormsRes.Result;
        }
        var scoutForms = scoutFormsRes.Value!
            .Where(f => f.DateTime >= analyticsSettings.StartDate && f.DateTime <= analyticsSettings.EndDate).ToList();

        var scoutFormFormat = await scoutFormFormatUnitOfWork.ScoutFormFormats.GetWithFieldsByYear(year, ScoutFormType.MainGame);
        if (scoutFormFormat.Result is not null)
        {
            return scoutFormFormat.Result;
        }

        foreach (var fieldsInStage in scoutFormFormat.Value!.FieldsInStages)
        {
            calculatedScoutDataByStages.Add(fieldsInStage.Name,
                TeamDataAnalyzer.CalculateDataFor(fieldsInStage, scoutForms, _ => true));
        }
        
        
        
        var scoutFormsPitRes = await scoutFormUnitOfWork.ScoutForms.GetAllByTeamWithData(teamNumber, eventKey, ScoutFormType.Pit);
        if (scoutFormsPitRes.Result is not null)
        {
            return scoutFormsPitRes.Result;
        }
        
        var scoutFormsPit = scoutFormsPitRes.Value!.ToList();
        
        var scoutFormFormatPit = await scoutFormFormatUnitOfWork.ScoutFormFormats.GetWithFieldsByYear(year, ScoutFormType.Pit);
        if (scoutFormFormatPit.Result is not null)
        {
            return scoutFormFormatPit.Result;
        }
        
        foreach (var fieldsInStage in scoutFormFormatPit.Value!.FieldsInStages)
        {
            calculatedScoutDataByStagesPit.Add(fieldsInStage.Name,
                TeamDataAnalyzer.CalculateDataFor(fieldsInStage, scoutFormsPit, _ => true));
        }
        
        return Ok(new AnalyticsTeamResult
        {
            CalculatedScoutDataByStages = calculatedScoutDataByStages,
            CalculatedScoutDataByStagesPit = calculatedScoutDataByStagesPit
        });
    }
    
    

}
