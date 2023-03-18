using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using OnyxScoutApplication.Shared.Other.Analyzers.TeamData;

namespace OnyxScoutApplication.Shared.Other.Analyzers
{
    public class TeamsAnalyzer
    {
        // [Inject]
        // // ReSharper disable once UnusedAutoPropertyAccessor.Local
        // private NavigationManager NavigationManager { get; set; }
        //
        // [Inject]
        // // ReSharper disable once UnusedAutoPropertyAccessor.Local
        // private NotificationManager NotificationManager { get; set; }

        private readonly List<Team> teams;

        private readonly List<FormDto> scoutForms;

        private readonly ScoutFormFormatDto scoutFormFormatDto;

        private readonly EventAnalyticSettingsDto eventAnalyticSettings;

        public TeamsAnalyzer(List<Team> teams, List<FormDto> scoutForms, ScoutFormFormatDto scoutFormFormatDto,
            EventAnalyticSettingsDto eventAnalyticSettings)
        {
            this.teams = teams;
            this.scoutForms = scoutForms;
            this.scoutFormFormatDto = scoutFormFormatDto;
            this.eventAnalyticSettings = eventAnalyticSettings;
        }

        //  public Func<FormDto, List<FormDataDto>> GetTargetList { get; set; }


        //public List<ExpandoObject> CalculatedTeamsData { get; private set; }

        // public List<ColumnField> ColumnsFields { get; private set; }

        private List<FieldDto> scoutFormFieldsToCalculate;

        public AnalyticsResult Calc()
        {
            scoutFormFieldsToCalculate = scoutFormFormatDto.FieldsInStages.SelectMany(i => i.Fields.WithCascadeFields()
                .Where(f => f.FieldType != FieldType.TextField)).ToList();
            var columnsFields = scoutFormFieldsToCalculate.Select(i => new ColumnField
                { Name = i.Name, MarkupName = new MarkupString(i.Name), Id = i.Id.ToString() }).ToList();
            if (eventAnalyticSettings != null)
            {
                foreach (var combinedField in eventAnalyticSettings.CombinedFields)
                {
                    ColumnField newColumnField = new ColumnField();
                    FieldDto lastField = null;
                    foreach (var field in combinedField.Fields)
                    {
                        if (scoutFormFieldsToCalculate.Any(i => i.Id == field.Id))
                        {
                            lastField = field;
                        }
                        else
                        {
                            // NavigationManager.NavigateTo("EventAnalytics/Settings");
                            // await NotificationManager.NotifyAsync("Please update the event settings",
                            //     $"Missing scout forms field: {field.Name}", NotificationType.Warning);
                        }
                    }

                    if (combinedField.Fields.Count <= 1) continue;

                    newColumnField.Name = combinedField.Name;
                    newColumnField.MarkupName = combinedField.MarkupName;
                    newColumnField.Id = combinedField.Id;
                    columnsFields.Insert(
                        columnsFields.IndexOf(columnsFields.FirstOrDefault(i => i.Id == lastField?.Id.ToString())) + 1,
                        newColumnField);
                }
            }

            var data = CalculateData();
            AnalyticsResult analyticsResult = new AnalyticsResult
            {
                CalculatedTeamsData = data,
                ColumnsFields = columnsFields
            };
            return analyticsResult;
        }


        private List<ExpandoObject> CalculateData()
        {
            var data = new List<ExpandoObject>();
            foreach (var team in teams)
            {
                var teamScoutForms = scoutForms.Where(i => i.TeamNumber == team.TeamNumber).OrderBy(i => i.MatchNumber)
                    .ToList();
                List<TeamFieldAverage> avgs = TeamDataAnalyzer
                    .CalculateDataFor(scoutFormFormatDto, teamScoutForms, _ => true).ToList();

                IDictionary<string, object> rows = new ExpandoObject();

                rows.Add("TeamNumber", team.TeamNumber);
                rows.Add("Nickname", team.Nickname);

                foreach (var avg in avgs)
                {
                    var teamAvg = avg;
                    rows.Add(avg.Field.Id!.ToString(), teamAvg.GetFormattedAverage().Value);
                    rows.Add("RawValue" + avg.Field.Id, teamAvg.GetRelativeValue());
                }

                if (eventAnalyticSettings != null)
                {
                    foreach (var combinedField in eventAnalyticSettings.CombinedFields)
                    {
                        switch (combinedField.CombinedFieldsType)
                        {
                            case CombinedFieldsType.Sum:
                                CalculateFieldSum(combinedField, rows, teamScoutForms);
                                break;
                            // case CombinedFieldsType.Avg:
                            //     CalculateFieldAvg(combinedField, rows);
                            //     break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }

                data.Add((ExpandoObject)rows);
            }

            return data;
        }

        private static void CalculateFieldSum(CombinedFieldsDto combinedField, IDictionary<string, object> rows,
            List<FormDto> teamScoutForms)
        {
            string sums = "";
            foreach (var scoutForm in teamScoutForms)
            {
                var allData = scoutForm.FormDataInStages.SelectMany(i => i.FormData.WithCascadeData()).ToList();
                double formSum = 0;
                foreach (var field in combinedField.Fields)
                {
                    var data = allData.FirstOrDefault(i => i.Field.Id == field.Id);
                    if (data is null)
                    {
                        Console.Error.WriteLine(
                            $"Missing data in scout form in match: {scoutForm.MatchNumber}, data name: {field.Name}");
                        continue;
                    }

                    if (data.NumericValue is null)
                    {
                        continue;
                    }

                    formSum += data.NumericValue.Value;
                }

                sums += $"{formSum},";
            }


            double sum = 0;
            foreach (var field in combinedField.Fields)
            {
                if (rows.ContainsKey("RawValue" + field.Id))
                {
                    sum += (double)rows["RawValue" + field.Id];
                }
                else
                {
                    Console.WriteLine(
                        $"Warning, some scouts forms missing some data to calculate combined averages ({field.Name}) ");
                }
            }

            rows.Add(combinedField.Id, new MarkupString(Math.Round(sum, 2) + "<br />" + sums).Value);
            rows.Add("RawValue" + combinedField.Id, sum);
        }

        // private static void CalculateFieldAvg(CombinedFieldsDto combinedField, IDictionary<string, object> rows)
        // {
        //     double sumAvg = 0;
        //     int index = 0;
        //     foreach (var field in combinedField.Fields)
        //     {
        //         if (rows.ContainsKey("RawValue" + field.Id))
        //         {
        //             sumAvg += (double)rows["RawValue" + field.Id];
        //             index++;
        //         }
        //         else
        //         {
        //             Console.WriteLine(
        //                 $"Warning, some scouts forms missing some data to calculate combined averages ({field.Name}) ");
        //         }
        //     }
        //
        //     sumAvg /= index;
        //     rows.Add(combinedField.Id, sumAvg);
        //     rows.Add("RawValue" + combinedField.Id, sumAvg);
        // }
    }

    public class ColumnField
    {
        public string Name { get; set; }

        public MarkupString MarkupName { get; set; }

        public string Id { get; set; }
    }
}
