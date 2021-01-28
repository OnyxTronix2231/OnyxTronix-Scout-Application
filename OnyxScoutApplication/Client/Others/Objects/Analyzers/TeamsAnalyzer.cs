using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public abstract class TeamsAnalyzer : ComponentBase
    {
        [Inject]
        TeamDataAnalyzer TeamDataAnalyzer { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        NotificationManager NotificationManager { get; set; }
        
        [Parameter]
        public List<Team> Teams { get; set; }

        [Parameter]
        public List<ScoutFormDto> ScoutForms { get; set; }

        [Parameter]
        public List<FieldDto> Fields { get; set; }

        [Parameter]
        public Func<ScoutFormDto, List<ScoutFormDataDto>> GetTragetList { get; set; }

        [Parameter]
        public EventAnalyticSettingsDto EventAnalyticSettings { get; set; }

        public List<ExpandoObject> CalculatedTeamsData { get; set; }

        public List<ColumnField> ColumnsFields { get; set; }

       // private List<FieldDto> scoutFormFieldsToCalculate;

        protected override void OnParametersSet()
        {
            //List<FieldDto> combinedFields = new List<FieldDto>();
            // scoutFormFieldsToCalculate = new List<FieldDto>(Fields);
            ColumnsFields = Fields.Select(i => new ColumnField() { Name = i.Name, MarupName = new MarkupString(i.Name), NameId = i.NameId } ).ToList();
            if (EventAnalyticSettings != null)
            {
                foreach (var combinedField in EventAnalyticSettings.CombinedFields)
                {
                    ColumnField newColumnField = new ColumnField();
                    FieldDto lastField = null;
                    foreach (var field in combinedField.Fields)
                    {
                        if (Fields.Any(i => i.NameId == field.NameId))
                        {
                            lastField = field;
                        }
                        else
                        {
                            NavigationManager.NavigateTo("EventAnalytics/Settings");
                            NotificationManager.Notify("Please update the event settings", $"Missing scout forms field: {field.NameId}", NotificationType.Warning);
                        }
                    }
                    if (combinedField.Fields.Count > 1)
                    {
                        newColumnField.Name = combinedField.Name;
                        newColumnField.MarupName = combinedField.MarupName;
                        newColumnField.NameId = combinedField.NameId;
                        //combinedFields.Add(newColumnField);
                        ColumnsFields.Insert(ColumnsFields.IndexOf(ColumnsFields.FirstOrDefault(i => i.NameId == lastField.NameId)) + 1, newColumnField);
                    }
                }
            }
            CalculateData();
        }

        public void CalculateData() { 
            var data = new List<ExpandoObject>();
            foreach (var team in Teams)
            {
                List<TeamFieldAverage> avgs = TeamDataAnalyzer.CalculateDataFor(Fields, ScoutForms.Where(i => i.TeamNumber == team.TeamNumber).ToList(), GetTragetList, s => true).ToList();

                IDictionary<String, Object> row = new ExpandoObject();

                row.Add("TeamNumber", team.TeamNumber);
                row.Add("Nickname", team.Nickname);

                foreach (var field in Fields)
                {

                    var teamAvg = avgs.First(i => i.Field.NameId == field.NameId);
                    row.Add(field.NameId, teamAvg.GetFormatedAverage().Value);
                    row.Add("RawValue" + field.NameId, teamAvg.GetRelativeValue());
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
                            if (row.ContainsKey("RawValue" + field.NameId))
                            {
                                sumAvg += (double)row["RawValue" + field.NameId];
                                fieldName += field.NameId;
                                index++;
                            }
                            else
                            {
                                Console.WriteLine($"Warning, some scouts forms missing some data to calculate combined averages ({field.NameId}) ");
                            }
                        }
                        sumAvg /= index;
                        row.Add(fieldName, sumAvg);
                        row.Add("RawValue" + fieldName, sumAvg);
                    }
                }
                data.Add(row as ExpandoObject);
            }
            CalculatedTeamsData = data;
        }

    }

    public class ColumnField
    {
        public string Name { get; set; }

        public MarkupString MarupName { get; set; }

        public string NameId { get; set; }
    }
}
