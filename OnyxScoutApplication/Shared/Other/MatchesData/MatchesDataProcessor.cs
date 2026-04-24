using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;
using OnyxScoutApplication.Shared.Other.Analyzers;

namespace OnyxScoutApplication.Shared.Other.MatchesData;

public class MatchesDataProcessor
{
    private readonly List<Team> teams;

    private readonly List<FormDto> scoutForms;

    private readonly ScoutFormFormatDto scoutFormFormatDto;

    private readonly EventAnalyticSettingsDto eventAnalyticSettings;

    public MatchesDataProcessor(List<Team> teams, List<FormDto> scoutForms, ScoutFormFormatDto scoutFormFormatDto,
        EventAnalyticSettingsDto eventAnalyticSettings)
    {
        this.teams = teams;
        this.scoutForms = scoutForms;
        this.scoutFormFormatDto = scoutFormFormatDto;
        this.eventAnalyticSettings = eventAnalyticSettings;
    }

    private List<FieldDto> scoutFormFieldsToCalculate;

    public AnalyticsResult GetMatchesData()
    {
        scoutFormFieldsToCalculate = scoutFormFormatDto.FieldsInStages.SelectMany(i => i.Fields.WithCascadeFields()
        ).ToList();
        var columnsFields = scoutFormFieldsToCalculate.Select(i => new ColumnField
            { Name = i.Name, MarkupName = new MarkupString(i.Name), Id = i.Id.ToString() }).ToList();

        // columnsFields.Insert(0, new ColumnField
        //     { Name = "Team", MarkupName = new MarkupString("Team"), Id = "Team" });
        // columnsFields.Insert(1, new ColumnField
        //     { Name = "Match N.", MarkupName = new MarkupString("Match N."), Id = "Match N." });

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
            ColumnsFields = columnsFields,
            NumberOfCalculatedForms = scoutForms.Count
        };
        return analyticsResult;
    }

    private List<ExpandoObject> CalculateData()
    {
        var data = new List<ExpandoObject>();
        foreach (var scoutForm in scoutForms)
        {
            if (scoutForm.MatchNumber == 49 && scoutForm.TeamNumber == 5987)
            {
                Console.WriteLine("");
            }
            var team = teams.FirstOrDefault(t => t.TeamNumber == scoutForm.TeamNumber);
            if (team is null)
            {
                continue;
            }

            var isNew = false;
            IDictionary<string, object> row = data.FirstOrDefault(d =>
            {
                IDictionary<string, object> dd = d;
                return dd["TeamNumber"].ToString() == team.TeamNumber.ToString() &&
                       dd["Match N."].ToString() == scoutForm.MatchNumber.ToString();
            });
            if (row is null)
            {
                isNew = true;
                row = new ExpandoObject();
                row.Add("TeamNumber", team.TeamNumber);
                row.Add("Nickname", team.Nickname);
                row.Add("Type", scoutForm.Type);
                row.Add("Match N.", scoutForm.MatchNumber);
            }
            else if(row["Type"].ToString() == scoutForm.Type.ToString())
            {
                Console.WriteLine($"Duplicate scout form found of type {scoutForm.Type}, match: @{scoutForm.MatchNumber}, team: @{team.TeamNumber}");
                continue;
            }
            

            foreach (var formData in scoutForm.FormDataInStages.SelectMany(i => i.FormData.WithCascadeData()))
            {
                string value;
                switch (formData.Field.FieldType)
                {
                    case FieldType.Timer:
                    case FieldType.Integer:
                        value = formData.NumericValue.ToString();
                        break;
                    case FieldType.Boolean:
                    case FieldType.CascadeField:
                        value = formData.BooleanValue ? "1" : "0";
                        break;
                    case FieldType.OptionSelect:
                    case FieldType.MultipleChoice:
                        if (formData.SelectedOptions is not null)
                            value = string.Join(", ", formData.SelectedOptions.Select(opt => opt.Index));
                        else
                            value = "";
                        break;
                    case FieldType.BooleanChooser:
                        value = formData.StringValue is not null ? formData.StringValue == "True" ? "1" : "0" : "";
                        break;
                    case FieldType.TextField:
                        value = formData.StringValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                row.Add("RawValue" + formData.Field.Id, value);
            }

            eventAnalyticSettings?.CombinedFields.ForEach(combinedFields =>
            {
                double? sum = null;
                foreach (var field in combinedFields.Fields)
                {
                    var res = scoutForm.FormDataInStages.SelectMany(i => i.FormData.WithCascadeData())
                        .FirstOrDefault(i => i.Field.Id == field.Id)?.NumericValue;
                    if (res is null) 
                        continue;
                    sum ??= 0;
                    sum += res;
                }

                if(sum is not null)
                    row.Add("RawValue" + combinedFields.Id, sum);
            });

            if(isNew)
                data.Add((ExpandoObject)row);
        }


        return data;
    }
}
