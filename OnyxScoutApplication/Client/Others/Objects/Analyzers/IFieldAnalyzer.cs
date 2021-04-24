using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public interface IFieldAnalyzer
    {
        TeamFieldAverage Analyze(IEnumerable<ScoutFormDto> scoutForms, FieldDto field,
            Func<ScoutFormDto, IEnumerable<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount);
    }
}
