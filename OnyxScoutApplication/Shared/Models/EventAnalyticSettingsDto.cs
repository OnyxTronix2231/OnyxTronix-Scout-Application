using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models
{
    public class EventAnalyticSettingsDto
    {
        public List<CombinedFieldsDto> CombinedFields { get; set; } = new();
    }

    public class CombinedFieldsDto
    {
        public List<FieldDto> Fields { get; set;} = new();

        public CombinedFieldsType CombinedFieldsType { get; set; }

        public string Name { get; set; }

        public MarkupString MarkupName => new(Name);

        public string Id
        {
            get { return string.Concat(Fields.Select(i => ";" + i.Id + ";")); }
        }
    }

    public enum CombinedFieldsType
    {
        Sum,
        // Avg
    }
}
