using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class OptionSelectAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
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
                if (string.IsNullOrWhiteSpace(scoutFormData.StringValue))
                    continue;
                if (optionsCount.ContainsKey(scoutFormData.StringValue))
                {
                    optionsCount[scoutFormData.StringValue] = optionsCount[scoutFormData.StringValue]++;
                }
                else
                {
                    optionsCount.Add(scoutFormData.StringValue, 1);
                }
            }

            foreach (var key in field.Options)
            {
                float count = 0;
                if (optionsCount.ContainsKey(key))
                {
                    count = optionsCount[key];
                }

                fieldAverage.OptionsAvarage.Add(key, new Tuple<float, float>(count, totalCount));
            }

            return fieldAverage;
        }
    }
}
