using OnyxScoutApplication.Client.Others.Objects.Analyzers;
using OnyxScoutApplication.Client.Others.Objects.TeamData;
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
        public List<TeamFieldAverage> CalculateDataFor(List<FieldDto> fields, List<ScoutFormDto> scoutForms, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            List<TeamFieldAverage> averagaes = new List<TeamFieldAverage>();
            for (int i = 0; i < fields.Count; i++)
            {
                if (fields[i].FieldType != FieldType.TextField)
                {
                    averagaes.Add(GetAvgFor(fields[i], scoutForms, getTragetList, shouldCount));
                    if (fields[i].FieldType == FieldType.CascadeField)
                    {
                        Func<ScoutFormDto, ScoutFormDataDto> getScoutFormData = scoutForm => getTragetList(scoutForm).FirstOrDefault(f => f.Field.Name == fields[i].Name);
                        averagaes.AddRange(CalculateDataFor(fields[i].CascadeFields, scoutForms, scoutForm => getScoutFormData(scoutForm).CascadeData, 
                            scoutForm => getScoutFormData(scoutForm) != null && getScoutFormData(scoutForm).BooleanValue));
                    }
                }
            }
            return averagaes;
        }

        private TeamFieldAverage GetAvgFor(FieldDto field, List<ScoutFormDto> data, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            if (field.FieldType == FieldType.Numeric)
            {
                NumricAnalayzer numricAnalayzer = new NumricAnalayzer();
                return numricAnalayzer.Analyze(data, field, getTragetList, shouldCount);
            }
            else if (field.FieldType == FieldType.Boolean || field.FieldType == FieldType.CascadeField)
            {
                BooleanAnalayzer booleanAnalayzer = new BooleanAnalayzer();
                return booleanAnalayzer.Analyze(data, field, getTragetList, shouldCount);
            } 
            else  if(field.FieldType == FieldType.OptionSelect)
            {
                OptionSelectAnalayzer optionSelectAnalayzer = new OptionSelectAnalayzer();
                Console.WriteLine("Analyzing option select");
                return optionSelectAnalayzer.Analyze(data, field, getTragetList, shouldCount);
            }
            return null;
        }
    }
}
