using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class TeamDataAnalyzer
    {
        public List<TeamFieldAverage> CalculateDataFor(IEnumerable<FieldDto> fields, List<ScoutFormDto> scoutForms,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
        {
            List<TeamFieldAverage> averages = new List<TeamFieldAverage>();
            foreach (var field in fields.Where(field => field.FieldType != FieldType.TextField))
            {
                averages.Add(GetAvgFor(field, scoutForms, getTargetList, shouldCount));

                if (field.FieldType != FieldType.CascadeField)
                    continue;

                ScoutFormDataDto GetScoutFormData(ScoutFormDto scoutForm) => getTargetList(scoutForm)
                    .FirstOrDefault(f => f.Field.NameId == field.NameId);

                averages.AddRange(CalculateDataFor(field.CascadeFields, scoutForms,
                    scoutForm => GetScoutFormData(scoutForm).CascadeData,
                    scoutForm =>
                        GetScoutFormData(scoutForm) != null && GetScoutFormData(scoutForm).BooleanValue));
            }

            return averages;
        }

        private TeamFieldAverage GetAvgFor(FieldDto field, List<ScoutFormDto> data,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
        {
            if (field.FieldType == FieldType.Numeric)
            {
                NumericalAnalyzer numericAnalyzer = new NumericalAnalyzer();
                return numericAnalyzer.Analyze(data, field, getTargetList, shouldCount);
            }
            else if (field.FieldType == FieldType.Boolean || field.FieldType == FieldType.CascadeField)
            {
                BooleanAnalyzer booleanAnalyzer = new BooleanAnalyzer();
                return booleanAnalyzer.Analyze(data, field, getTargetList, shouldCount);
            }
            else if (field.FieldType == FieldType.OptionSelect)
            {
                OptionSelectAnalyzer optionSelectAnalyzer = new OptionSelectAnalyzer();
                return optionSelectAnalyzer.Analyze(data, field, getTargetList, shouldCount);
            }
            else if (field.FieldType == FieldType.MultipleChoice)
            {
                MultipleChoiceAnalyzer multipleChoiceAnalyzer = new MultipleChoiceAnalyzer();
                return multipleChoiceAnalyzer.Analyze(data, field, getTargetList, shouldCount);
            }

            return null;
        }
    }
}
