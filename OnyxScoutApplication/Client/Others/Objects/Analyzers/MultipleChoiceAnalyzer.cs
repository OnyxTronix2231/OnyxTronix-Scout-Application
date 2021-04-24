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
        public TeamFieldAverage Analyze(IEnumerable<FormDto> scoutForms, FieldDto field,
            Func<FormDto, IEnumerable<FormDataDto>> getTargetList, Func<FormDto, bool> shouldCount)
        {
            OptionSelectTeamFieldAverage fieldAverage = new OptionSelectTeamFieldAverage(field);
            int totalCount = 0;
            Dictionary<string, int> optionsCount = new Dictionary<string, int>();
            foreach (var scoutFormData in from scoutForm in scoutForms
                where shouldCount(scoutForm)
                select getTargetList(scoutForm).FirstOrDefault(i => i.Field.NameId == field.NameId)
                into scoutFormData
                where scoutFormData != null
                select scoutFormData)
            {
                totalCount++;
                foreach (var selectedOption in scoutFormData.SelectedOptions)
                {
                    if (string.IsNullOrWhiteSpace(selectedOption))
                    {
                        continue;
                    }
                    if (optionsCount.ContainsKey(selectedOption))
                    {
                        optionsCount[selectedOption] = optionsCount[scoutFormData.StringValue]++;
                    }
                    else
                    {
                        optionsCount.Add(selectedOption, 1);
                    }
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
