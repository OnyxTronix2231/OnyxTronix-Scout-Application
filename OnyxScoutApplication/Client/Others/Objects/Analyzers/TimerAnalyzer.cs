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
    public class TimerAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            NumericTeamFieldAverage fieldAverage = new NumericTeamFieldAverage(field);
            TimeSpan average = TimeSpan.Zero;
            int count = 0;
            foreach (var formData in allFormData.Where(i => i.Field.Id == field.Id))
            {
                if (!shouldCount(formData) || formData.TimeSpanValue == null)
                {
                    continue;
                }
                average += formData.TimeSpanValue.Value;
                count++;
                fieldAverage.Values.Add((float) formData.TimeSpanValue.Value.TotalSeconds);
            }
            if (count == 0)
            {
                fieldAverage.Average = float.NaN;
                return fieldAverage;
            }
            fieldAverage.Average = (float) (average / count).TotalSeconds;
            return fieldAverage;
        }
    }
}
