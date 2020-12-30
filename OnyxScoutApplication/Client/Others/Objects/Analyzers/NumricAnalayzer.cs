using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class NumricAnalayzer : IFieldAnalyzer
    {
        public FieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            FieldAverage fieldAverage = new FieldAverage(field);
            float avarge = 0;
            int count = 0;
            foreach (var scoutForm in scoutForms)
            {
                if (shouldCount(scoutForm))
                {
                    ScoutFormDataDto scoutFormData = getTragetList(scoutForm).FirstOrDefault(i => i.Field.Name == field.Name);
                    if (scoutFormData != null)
                    {
                        if (scoutFormData.NumricValue != null)
                        {
                            int value = (int)scoutFormData.NumricValue;
                            avarge += value;
                            count++;
                            fieldAverage.Values.Add(value);
                        }
                    }
                }
            }
            fieldAverage.Average = avarge / count;
            return fieldAverage;
        }
    }
}
                
        


