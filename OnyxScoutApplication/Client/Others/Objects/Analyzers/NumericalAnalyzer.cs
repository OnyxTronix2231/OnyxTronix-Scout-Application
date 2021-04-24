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
    public class NumericalAnalyzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(IEnumerable<FormDto> scoutForms, FieldDto field,
            Func<FormDto, IEnumerable<FormDataDto>> getTargetList, Func<FormDto, bool> shouldCount)
        {
            NumericTeamFieldAverage fieldAverage = new NumericTeamFieldAverage(field);
            float average = 0;
            int count = 0;
            foreach (var value in from scoutForm in scoutForms
                where shouldCount(scoutForm)
                select getTargetList(scoutForm).FirstOrDefault(i => i.Field.NameId == field.NameId)
                into scoutFormData
                where scoutFormData != null
                where scoutFormData.NumericValue != null
                select (int) scoutFormData.NumericValue)
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
