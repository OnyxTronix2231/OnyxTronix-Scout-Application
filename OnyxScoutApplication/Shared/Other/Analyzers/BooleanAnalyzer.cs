using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public class BooleanAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            BooleanTeamFieldAverage fieldAverage = new BooleanTeamFieldAverage(field);
            int trueCount = 0;
            int count = 0;
            foreach (var formData in allFormData.Where(i => i.Field.Id == field.Id))
            {
                if (!shouldCount(formData))
                {
                    continue;
                }
                count++;
                if (formData.BooleanValue)
                {
                    trueCount++;
                }
            }

            fieldAverage.TrueCount = trueCount;
            fieldAverage.TotalCount = count;
            return fieldAverage;
        }
    }
}
