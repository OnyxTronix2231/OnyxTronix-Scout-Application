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
    public class MultipleChoiceAnalyzer : IFieldAnalyzer
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

                if (formData.SelectedOptions is null)
                {
                    continue;
                }

                foreach (var selectedOption in formData.SelectedOptions)
                {
                    if (optionsCount.ContainsKey(selectedOption.Name))
                    {
                        optionsCount[selectedOption.Name]++;
                    }
                    else
                    {
                        optionsCount.Add(selectedOption.Name, 1);
                    }
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
