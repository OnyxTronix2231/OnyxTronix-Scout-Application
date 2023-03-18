using System;
using System.Collections.Generic;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public interface IFieldAnalyzer
    {
        TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount);
    }
}
