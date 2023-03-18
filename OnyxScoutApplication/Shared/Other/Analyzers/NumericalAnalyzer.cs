using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public class NumericalAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            NumericTeamFieldAverage fieldAverage = new NumericTeamFieldAverage(field);
            float average = 0;
            int count = 0;
            foreach (var formData in allFormData.Where(i => i.Field.Id == field.Id))
            {
                if (!shouldCount(formData) || formData.NumericValue == null)
                {
                    continue;
                }
                average += formData.NumericValue.Value;
                count++;
                fieldAverage.Values.Add(formData.NumericValue.Value);
            }

            fieldAverage.Average = average / count;
            return fieldAverage;
        }
    }
}
