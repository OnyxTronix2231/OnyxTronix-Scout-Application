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
    public class BooleanChooserAnalyzer : IFieldAnalyzer
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

                if (string.IsNullOrWhiteSpace(formData.StringValue))
                {
                    continue;
                }
                
                count++;
                if (formData.StringValue == "True")
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
