using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
