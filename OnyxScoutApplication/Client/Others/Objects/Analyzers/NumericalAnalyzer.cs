using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class NumericalAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
        {
            NumericTeamFieldAverage fieldAverage = new NumericTeamFieldAverage(field);
            float average = 0;
            int count = 0;
            foreach (var value in from scoutForm in scoutForms
                where shouldCount(scoutForm)
                select getTargetList(scoutForm).FirstOrDefault(i => i.Field.NameId == field.NameId)
                into scoutFormData
                where scoutFormData != null
                where scoutFormData.NumricValue != null
                select (int) scoutFormData.NumricValue)
            {
                average += value;
                count++;
                fieldAverage.Values.Add(value);
            }

            fieldAverage.Average = average / count;
            return fieldAverage;
        }
    }
}