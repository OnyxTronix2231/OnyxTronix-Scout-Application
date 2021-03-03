using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData;
using OnyxScoutApplication.Shared.Models;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public static class TeamDataAnalyzer
    {
        public static List<TeamFieldAverage> CalculateDataFor(IEnumerable<FieldDto> fields, List<ScoutFormDto> scoutForms,
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

        private static TeamFieldAverage GetAvgFor(FieldDto field, IEnumerable<ScoutFormDto> data,
            Func<ScoutFormDto, List<ScoutFormDataDto>> getTargetList, Func<ScoutFormDto, bool> shouldCount)
        {
            IFieldAnalyzer analyzer;
            switch (field.FieldType)
            {
                case FieldType.Numeric:
                    analyzer = new NumericalAnalyzer();
                    break;
                case FieldType.Boolean:
                case FieldType.CascadeField:
                    analyzer = new BooleanAnalyzer();
                    break;
                case FieldType.OptionSelect:
                    analyzer = new OptionSelectAnalyzer();
                    break;
                case FieldType.MultipleChoice:
                    analyzer = new MultipleChoiceAnalyzer();
                    break;
                case FieldType.None:
                    throw new NotSupportedException();
                case FieldType.TextField:
                    throw new NotSupportedException();
                default:
                    throw new NotSupportedException();
            }
            return analyzer.Analyze(data, field, getTargetList, shouldCount);
        }
    }
}
