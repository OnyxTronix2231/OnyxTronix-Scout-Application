using System;
using System.Collections.Generic;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Shared.Models
{
    public class FormDataInStageDto : IComparable<FormDataInStageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormId { get; set; }
        public ScoutFormDto ScoutForm { get; set; }
        public List<ScoutFormDataDto> FormData { get; set; } = new List<ScoutFormDataDto>();
        
        public int CompareTo(FormDataInStageDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
