using System.Collections.Generic;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers;

public class AnalyticsTeamResult
{
    public Dictionary<string, List<TeamFieldAverage>> CalculatedScoutDataByStages { get; set; }
        
    public Dictionary<string, List<TeamFieldAverage>> CalculatedScoutDataByStagesPit { get; set; }
}
