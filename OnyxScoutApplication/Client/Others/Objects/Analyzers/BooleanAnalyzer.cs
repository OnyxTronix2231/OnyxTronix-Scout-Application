using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class BooleanAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<ScoutFormDto> scoutForms, FieldDto field,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
        {
            BooleanTeamFieldAverage fieldAverage = new BooleanTeamFieldAverage(field);
            int trueCount = 0;
            int count = 0;
            foreach (var scoutFormData in from scoutForm in scoutForms
                where shouldCount(scoutForm)
                select getTargetList(scoutForm).FirstOrDefault(i => i.Field.NameId == field.NameId)
                into scoutFormData
                where scoutFormData != null
                select scoutFormData)
            {
                count++;
                if (scoutFormData.BooleanValue)
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
