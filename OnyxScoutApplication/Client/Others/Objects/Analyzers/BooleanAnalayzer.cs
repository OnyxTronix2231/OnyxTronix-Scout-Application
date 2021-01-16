using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class BooleanAnalayzer : IFieldAnalyzer
    {
        public TeamFieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            BooleanTeamFieldAverage fieldAverage = new BooleanTeamFieldAverage(field);
            int trueCount = 0;
            int count = 0;
            foreach (var scoutForm in scoutForms)
            {
                if (shouldCount(scoutForm))
                {
                    ScoutFormDataDto scoutFormData = getTragetList(scoutForm).FirstOrDefault(i => i.Field.NameId == field.NameId);
                    if (scoutFormData != null)
                    {
                        count++;
                        if (scoutFormData.BooleanValue)
                        {
                            trueCount++;
                        }
                    }
                }
            }
            fieldAverage.TrueCount = trueCount;
            fieldAverage.TotalCount = count;
            return fieldAverage;
        }
    }
}
                
        


