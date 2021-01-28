using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace OnyxScoutApplication.Shared.Models
{
    public class EventAnalyticSettingsDto
    {
        public int Id { get; set; }
        public List<CombinedFieldsDto> CombinedFields { get; set; } = new List<CombinedFieldsDto>();
    }

    public class CombinedFieldsDto
    {
        public List<FieldDto> Fields { get; set; } = new List<FieldDto>();

        public string Name
        {
            get
            {
                return string.Concat(Fields.Select(i => i.Name));
            }
        }

        public MarkupString MarupName
        {
            get
            {
                return new MarkupString(string.Join("<br />", Fields.Select(i => i.Name)));
            }
        }

        public string NameId
        {
            get
            {
                return string.Concat(Fields.Select(i => i.NameId));
            }
        }
    }
}
