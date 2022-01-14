using System;
using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataInStageDto : IComparable<FormDataInStageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormId { get; set; }
        public FormDto Form { get; set; }
        public List<FormDataDto> FormData { get; set; } = new List<FormDataDto>();
        
        public int CompareTo(FormDataInStageDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
