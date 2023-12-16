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
            .Where(f => f.FieldType != FieldType.TextField)).ToList();
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
            ColumnsFields = columnsFields
        };
        return analyticsResult;
    }

    private List<ExpandoObject> CalculateData()
    {
        var data = new List<ExpandoObject>();
        foreach (var scoutForm in scoutForms)
        {
            var team = teams.FirstOrDefault(t => t.TeamNumber == scoutForm.TeamNumber);
            if (team is null)
            {
                continue;
            }

            IDictionary<string, object> row = new ExpandoObject();

            row.Add("TeamNumber", team.TeamNumber);
            row.Add("Nickname", team.Nickname);
            row.Add("Match N.", scoutForm.MatchNumber);

            foreach (var formData in scoutForm.FormDataInStages.SelectMany(i => i.FormData.WithCascadeData()))
            {
                row.Add("RawValue" + formData.Field.Id,
                    formData.NumericValue?.ToString() ?? (formData.BooleanValue ? "1" : "0"));
            }

            eventAnalyticSettings?.CombinedFields.ForEach(combinedFields =>
            {
                double sum = 0;
                foreach (var field in combinedFields.Fields)
                {
                    sum += scoutForm.FormDataInStages.SelectMany(i => i.FormData.WithCascadeData())
                        .FirstOrDefault(i => i.Field.Id == field.Id)?.NumericValue ?? 0;
                }

                row.Add("RawValue" + combinedFields.Id, sum);
            });

            data.Add((ExpandoObject)row);
        }


        return data;
    }
}
