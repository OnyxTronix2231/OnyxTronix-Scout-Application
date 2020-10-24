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
                averagaes.Add(new FieldAverage { Field = fields[i], Average = GetAvgFor(fields[i].FieldType, i, scoutForms, getTragetList, shouldCount) });
                if (fields[i].FieldType == FieldType.CascadeField)
                {
                    averagaes.AddRange(CalculateDataFor(fields[i].CascadeFields, scoutForms, scoutForm => getTragetList(scoutForm)[i].CascadeData, scoutForm => getTragetList(scoutForm)[i].BooleanValue));
                }
            }
            return averagaes;
        }

        private float GetAvgFor(FieldType fieldType, int i, List<ScoutFormDto> data, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            if (fieldType == FieldType.Numeric)
            {
                float avarge = 0;
                int count = 0;
                foreach (var scoutForm in data)
                {
                    if (shouldCount(scoutForm))
                    {
                        avarge += (int)getTragetList(scoutForm)[i].NumricValue;
                        count++;
                    }
                }
                return avarge /= count;
            }
            else if (fieldType == FieldType.Boolean)
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
                return trueAvarage /= count;
            } 
            else if(fieldType == FieldType.CascadeField)
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
                return trueAvarage /= count;
            }
            return 0;
        }
    }
}
