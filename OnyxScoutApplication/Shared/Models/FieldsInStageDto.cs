using System;
using System.Collections.Generic;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Shared.Models
{
    public class FieldsInStageDto : IComparable<FieldsInStageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormFormatId { get; set; }
        public ScoutFormFormatDto ScoutFormFormat { get; set; }
        public SortedList<FieldDto> Fields { get; set; }

        public int CompareTo(FieldsInStageDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
