using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public interface IFieldAnalyzer
    {
        TeamFieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount);
    }
}
