using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace OnyxScoutApplication.Shared.Models
{
    public class EventAnalyticSettingsDto
    {
        public List<CombinedFieldsDto> CombinedFields { get; } = new List<CombinedFieldsDto>();
    }

    public class CombinedFieldsDto
    {
        public List<FieldDto> Fields { get; } = new List<FieldDto>();

        public string Name
        {
            get
            {
                return string.Concat(Fields.Select(i => i.Name));
            }
        }

        public MarkupString MarkupName
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
