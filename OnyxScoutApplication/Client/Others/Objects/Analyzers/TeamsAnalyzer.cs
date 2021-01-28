using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Managers;
using OnyxScoutApplication.Client.Others.Objects.TeamData;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<FieldDto> ColumnsFields { get; set; }

        [Parameter]
        public Func<ScoutFormDto, List<ScoutFormDataDto>> GetTragetList { get; set; }

        [Parameter]
        public EventAnalyticSettingsDto EventAnalyticSettings { get; set; }

        public List<ExpandoObject> CalculatedTeamsData { get; set; }

        private List<FieldDto> scoutFormFieldsToCalculate;

        protected override void OnParametersSet()
        {
            List<FieldDto> combinedFields = new List<FieldDto>();
            scoutFormFieldsToCalculate = new List<FieldDto>(ColumnsFields);
            if (EventAnalyticSettings != null)
            {
                foreach (var combinedField in EventAnalyticSettings.CombinedFields)
                {
                    FieldDto newField = new FieldDto();
                    FieldDto lastField = null;
                    string fieldName = "";
                    foreach (var field in combinedField.Fields)
                    {
                            fieldName += field.NameId;
                            lastField = field;
                    }
                    if (combinedField.Fields.Count > 1)
                    {
                        newField.Name = fieldName;
                        combinedFields.Add(newField);
                        ColumnsFields.Insert(ColumnsFields.IndexOf(ColumnsFields.FirstOrDefault(i => i.NameId == lastField.NameId)) + 1, newField);
                    }
                }
            }
            CalculateData();
        }

        public void CalculateData() { 
            var data = new List<ExpandoObject>();
            foreach (var team in Teams)
            {
                List<TeamFieldAverage> avgs = TeamDataAnalyzer.CalculateDataFor(scoutFormFieldsToCalculate, ScoutForms.Where(i => i.TeamNumber == team.TeamNumber).ToList(), GetTragetList, s => true).ToList();

                IDictionary<String, Object> row = new ExpandoObject();

                row.Add("TeamNumber", team.TeamNumber);
                row.Add("Nickname", team.Nickname);

                foreach (var field in scoutFormFieldsToCalculate)
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
}
