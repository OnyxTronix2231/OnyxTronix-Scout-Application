using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class BooleanAnalayzer : IFieldAnalyzer
    {
        public FieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            FieldAverage fieldAverage = new FieldAverage(field);
            float trueCount = 0;
            int count = 0;
            foreach (var scoutForm in scoutForms)
            {
                if (shouldCount(scoutForm))
                {
                    ScoutFormDataDto scoutFormData = getTragetList(scoutForm).FirstOrDefault(i => i.Field.Name == field.Name);
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
            fieldAverage.Average = trueCount / count;
            return fieldAverage;
        }
    }
}
                
        


