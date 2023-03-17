using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public class TextAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            TextTeamFieldViewer fieldAverage = new TextTeamFieldViewer(field);
            foreach (var formData in allFormData.Where(i => i.Field.Id == field.Id))
            {
                fieldAverage.Texts.Add(formData.StringValue);
            }

            return fieldAverage;
        }
    }
}
