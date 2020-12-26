using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class TeamDataAnalyzer
    {
        public List<FieldAverage> CalculateDataFor(List<FieldDto> fields, List<ScoutFormDto> scoutForms, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            List<FieldAverage> averagaes = new List<FieldAverage>();
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].FieldType != FieldType.TextField)
                {
                    averagaes.Add(GetAvgFor(fields[i], i, scoutForms, getTragetList, shouldCount));
                    if (fields[i].FieldType == FieldType.CascadeField)
                    {
                        averagaes.AddRange(CalculateDataFor(fields[i].CascadeFields, scoutForms, scoutForm => getTragetList(scoutForm)[i].CascadeData, scoutForm => getTragetList(scoutForm)[i].BooleanValue));
                    }
                }
            }
            return averagaes;
        }

        private FieldAverage GetAvgFor(FieldDto field, int i, List<ScoutFormDto> data, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            FieldAverage fieldAverage = new FieldAverage(field);
            if (field.FieldType == FieldType.Numeric)
            {
                float avarge = 0;
                int count = 0;
                foreach (var scoutForm in data)
                {
                    if (shouldCount(scoutForm))
                    {
                        if (getTragetList(scoutForm)[i].NumricValue != null)
                        {
                            int value = (int)getTragetList(scoutForm)[i].NumricValue;
                            avarge += value;
                            count++;
                            fieldAverage.Values.Add(value);
                        }
                    }
                }
                fieldAverage.Average = avarge /= count;
            }
            else if (field.FieldType == FieldType.Boolean || field.FieldType == FieldType.CascadeField)
            {
                float trueAvarage = 0;
                int count = 0;
                foreach (var scoutForm in data)
                {
                    if (shouldCount(scoutForm))
                    {
                        count++;
                        if (getTragetList(scoutForm)[i].BooleanValue)
                        {
                            trueAvarage++;
                        }
                    }
                }
                fieldAverage.Average = trueAvarage /= count;
            }
            return fieldAverage;
        }
    }
}
