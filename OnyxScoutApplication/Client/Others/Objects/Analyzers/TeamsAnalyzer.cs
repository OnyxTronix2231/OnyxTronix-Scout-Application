using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using OnyxScoutApplication.Client.Others.Extensions;
using OnyxScoutApplication.Client.Others.Objects.Analyzers.TeamData;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public abstract class TeamsAnalyzer : ComponentBase
    {
        [Inject]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private NotificationManager NotificationManager { get; set; }

        [Parameter]
        public List<Team> Teams { get; set; }

        [Parameter]
        public List<FormDto> ScoutForms { get; set; }

        [Parameter]
        public ScoutFormFormatDto ScoutFormFormatDto { get; set; }

        [Parameter]
        public Func<FormDto, List<FormDataDto>> GetTargetList { get; set; }

        [Parameter]
        public EventAnalyticSettingsDto EventAnalyticSettings { get; set; }

        protected List<ExpandoObject> CalculatedTeamsData { get; private set; }

        protected List<ColumnField> ColumnsFields { get; private set; }
        
        private List<FieldDto> scoutFormFieldsToCalculate;

        protected override void OnParametersSet()
        {
            //List<FieldDto> combinedFields = new List<FieldDto>();
            // scoutFormFieldsToCalculate = new List<FieldDto>(Fields);
            scoutFormFieldsToCalculate = ScoutFormFormatDto.FieldsInStages.SelectMany(i => i.Fields.WithCascadeFields()
                .Where(f => f.FieldType != FieldType.TextField)).ToList();
            ColumnsFields = scoutFormFieldsToCalculate.Select(i => new ColumnField
                {Name = i.Name, MarkupName = new MarkupString(i.Name), Id = i.Id.ToString()}).ToList();
            if (EventAnalyticSettings != null)
            {
                foreach (var combinedField in EventAnalyticSettings.CombinedFields)
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
                            NavigationManager.NavigateTo("EventAnalytics/Settings");
                            NotificationManager.Notify("Please update the event settings",
                                $"Missing scout forms field: {field.Name}", NotificationType.Warning);
                        }
                    }

                    if (combinedField.Fields.Count <= 1) continue;

                    newColumnField.Name = combinedField.Name;
                    newColumnField.MarkupName = combinedField.MarkupName;
                    newColumnField.Id = combinedField.Id;
                    ColumnsFields.Insert(
                        ColumnsFields.IndexOf(ColumnsFields.FirstOrDefault(i => i.Id == lastField?.Id.ToString())) + 1,
                        newColumnField);
                }
            }

            CalculateData();
        }

        private void CalculateData()
        {
            var data = new List<ExpandoObject>();
            foreach (var team in Teams)
            {
                List<TeamFieldAverage> avgs = TeamDataAnalyzer.CalculateDataFor(ScoutFormFormatDto,
                    ScoutForms.Where(i => i.TeamNumber == team.TeamNumber).ToList(), s => true).ToList();

                IDictionary<string, object> rows = new ExpandoObject();

                rows.Add("TeamNumber", team.TeamNumber);
                rows.Add("Nickname", team.Nickname);

                foreach (var avg in avgs)
                {
                    var teamAvg = avg;
                    rows.Add(avg.Field.Id.ToString(), teamAvg.GetFormattedAverage().Value);
                    rows.Add("RawValue" + avg.Field.Id, teamAvg.GetRelativeValue());
                }

                if (EventAnalyticSettings != null)
                {
                    foreach (var combinedField in EventAnalyticSettings.CombinedFields)
                    {
                        double sumAvg = 0;
                        string fieldName = "";
                        int index = 0;
                        foreach (var field in combinedField.Fields)
                        {
                            if (rows.ContainsKey("RawValue" + field.Id))
                            {
                                sumAvg += (double) rows["RawValue" + field.Id];
                                fieldName += field.Id;
                                index++;
                            }
                            else
                            {
                                Console.WriteLine(
                                    $"Warning, some scouts forms missing some data to calculate combined averages ({field.Name}) ");
                            }
                        }

                        sumAvg /= index;
                        rows.Add(fieldName, sumAvg);
                        rows.Add("RawValue" + fieldName, sumAvg);
                    }
                }

                data.Add((ExpandoObject) rows);
            }

            CalculatedTeamsData = data;
        }
    }

    public class ColumnField
    {
        public string Name { get; set; }

        public MarkupString MarkupName { get; set; }

        public string Id { get; set; }
    }
}
