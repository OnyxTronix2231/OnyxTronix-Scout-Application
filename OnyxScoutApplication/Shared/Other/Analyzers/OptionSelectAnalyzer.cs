using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public class OptionSelectAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            OptionSelectTeamFieldAverage fieldAverage = new OptionSelectTeamFieldAverage(field);
            int totalCount = 0;
            Dictionary<string, int> optionsCount = new Dictionary<string, int>();
            foreach (var formData in allFormData.Where(i => i.Field.Id == field.Id))
            {
                if (!shouldCount(formData))
                {
                    continue;
                }
                
                totalCount++;
                
                if (formData.SelectedOptions is null || formData.SelectedOptions.Count == 0)
                {
                    continue;
                }
                
                if (optionsCount.ContainsKey(formData.SelectedOptions[0].Name))
                {
                    optionsCount[formData.SelectedOptions[0].Name]++;
                }
                else
                {
                    optionsCount.Add(formData.SelectedOptions[0].Name, 1);
                }
            }

            fieldAverage.TotalCount = totalCount;
            foreach (var option in field.Options)
            {
                int count = 0;
                if (optionsCount.ContainsKey(option.Name))
                {
                    count = optionsCount[option.Name];
                }

                fieldAverage.OptionsAverage.Add(option.Name, new OptionCalc{Count = count,
                    PercentWeight = option.PercentWeight});
            }

            return fieldAverage;
        }
    }
}
