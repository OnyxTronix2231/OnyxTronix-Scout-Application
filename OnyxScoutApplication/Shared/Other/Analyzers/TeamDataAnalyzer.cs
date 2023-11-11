using System;
using System.Collections.Generic;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public static class TeamDataAnalyzer
    {
        public static IEnumerable<TeamFieldAverage> CalculateDataFor(ScoutFormFormatDto scoutFormFormat,
            List<FormDto> forms, Func<FormDataDto, bool> shouldCount, bool shouldIncludeTextFields = false)
        {
            List<TeamFieldAverage> averages = new List<TeamFieldAverage>();
            foreach (var fieldsInStage in scoutFormFormat.FieldsInStages)
            {
                averages.AddRange(CalculateDataFor(fieldsInStage, forms, shouldCount, shouldIncludeTextFields));
            }

            return averages;
        }

        public static List<TeamFieldAverage> CalculateDataFor(FieldsInStageDto fieldsInStage,
            IEnumerable<FormDto> forms, Func<FormDataDto, bool> shouldCount, bool shouldIncludeTextFields = false)
        {
            List<TeamFieldAverage> averages = new List<TeamFieldAverage>();
            var stageInForm = forms.SelectMany(i => i.FormDataInStages).Where(ii => ii.Name == fieldsInStage.Name)
                .ToList();
            averages.AddRange(CalculateDataFor(fieldsInStage, stageInForm, shouldCount, shouldIncludeTextFields));
            return averages;
        }

        public static IEnumerable<TeamFieldAverage> CalculateDataFor(FieldsInStageDto fieldsInStage,
            IEnumerable<FormDataInStageDto> formDataInStage, Func<FormDataDto, bool> shouldCount,
            bool shouldIncludeTextFields = false)
        {
            List<TeamFieldAverage> averages = new List<TeamFieldAverage>();
            averages.AddRange(CalculateDataFor(
                fieldsInStage.Fields.Where(field => field.FieldType != FieldType.TextField || shouldIncludeTextFields),
                formDataInStage.SelectMany(i => i.FormData).
                    Where(field => field.Field.FieldType != FieldType.TextField || shouldIncludeTextFields).ToList(),
                shouldCount, shouldIncludeTextFields));
            return averages;
        }

        public static IEnumerable<TeamFieldAverage> CalculateDataFor(IEnumerable<FieldDto> fields,
            List<FormDataDto> formData, Func<FormDataDto, bool> shouldCount, bool shouldIncludeTextFields = false)
        {
            List<TeamFieldAverage> averages = new List<TeamFieldAverage>();
            foreach (var field in fields)
            {
                averages.Add(GetAvgFor(field, formData, shouldCount));

                if (field.FieldType != FieldType.CascadeField)
                    continue;

                averages.AddRange(CalculateDataFor(field.CascadeFields.Where(f =>
                        f.FieldType != FieldType.TextField || shouldIncludeTextFields),

                    formData.SelectMany(i => i.CascadeData).ToList(), data =>
                        formData.Where(i => i.Field.Id == field.Id).First(i =>
                            i.CascadeData.Contains(data)).BooleanValue,
                    shouldIncludeTextFields));
            }

            return averages;
        }

        private static TeamFieldAverage GetAvgFor(FieldDto field, List<FormDataDto> data
            , Func<FormDataDto, bool> shouldCount)
        {
            IFieldAnalyzer analyzer;
            switch (field.FieldType)
            {
                case FieldType.Timer:
                case FieldType.Integer:
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
                case FieldType.BooleanChooser:
                    analyzer = new BooleanChooserAnalyzer();
                    break;
                case FieldType.TextField:
                    analyzer = new TextAnalyzer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return analyzer.Analyze(data, field, shouldCount);
        }
    }
}
