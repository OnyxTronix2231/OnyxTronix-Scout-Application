using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class FieldDto : IComparable<FieldDto>
    {
        public int Id { get; set; }

        public int ScoutFormFormatId { get; set; }

        public string Name { get; set; }

        public string TextDefaultValue { get; set; }

        public bool BoolDefaultValue { get; set; }

        public int? NumericDefaultValue { get; set; }

        public bool CascadeConditionDefaultValue { get; set; }
        
        public TimeSpan? TimeSpanDefaultValue { get; set; }

        public int MyProperty { get; set; }

        public FieldType FieldType { get; set; }

        public int MaxValue { get; set; } = 9999;

        public int MinValue { get; set; }

        public bool Required { get; set; }

        public List<string> Options { get; set; } = new List<string>();

        public List<string> DefaultSelectedOptions { get; set; } = new List<string>();

        public int MaximumSelectionLength { get; set; }

        public List<FieldDto> CascadeFields { get; set; } = new List<FieldDto>();

        public int Index { get; set; }
        
        public int CompareTo(FieldDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
