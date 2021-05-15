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
                foreach (var selectedOption in formData.SelectedOptions)
                {
                    if (string.IsNullOrWhiteSpace(selectedOption.Name))
                    {
                        continue;
                    }
                    if (optionsCount.ContainsKey(selectedOption.Name))
                    {
                        optionsCount[selectedOption.Name] = optionsCount[selectedOption.Name]++;
                    }
                    else
                    {
                        optionsCount.Add(selectedOption.Name, 1);
                    }
                }
            }

            foreach (var key in field.Options.Select(i => i.Name))
            {
                float count = 0;
                if (optionsCount.ContainsKey(key))
                {
                    count = optionsCount[key];
                }

                fieldAverage.OptionsAverage.Add(key, new Tuple<float, float>(count, totalCount));
            }

            return fieldAverage;
        }
    }
}
