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
    public class OptionSelectAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDataDto> allFormData, FieldDto field,
            Func<FormDataDto, bool> shouldCount)
        {
            OptionSelectTeamFieldAverage fieldAverage = new OptionSelectTeamFieldAverage(field);
            int totalCount = 0;
            Dictionary<string, int> optionsCount = new Dictionary<string, int>();
            foreach (var formData in allFormData.Where(i => i.Field.NameId == field.NameId))
            {
                if (!shouldCount(formData))
                {
                    continue;
                }
                totalCount++;
                if (string.IsNullOrWhiteSpace(formData.StringValue))
                    continue;
                if (optionsCount.ContainsKey(formData.StringValue))
                {
                    optionsCount[formData.StringValue] = optionsCount[formData.StringValue]++;
                }
                else
                {
                    optionsCount.Add(formData.StringValue, 1);
                }
            }

            foreach (var key in field.Options)
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
