using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models
{
    public class FieldDto
    {
        public int Id { get; set; }

        public int ScoutFormFormatId { get; set; }

        [JsonIgnore]
        public ScoutFormFormat ScoutFormFormat { get; set; }

        public string Name { get; set; }

        public string TextDefaultValue { get; set; }

        public bool BoolDefaultValue { get; set; }

        public int? NumericDefaultValue { get; set; }

        public bool CascadeConditionDefaultValue { get; set; }

        public int MyProperty { get; set; }

        public FieldType FieldType { get; set; }

        public string FieldStage { get; set; }

        public int MaxValue { get; set; } = 9999;

        public int MinValue { get; set; }

        public bool Required { get; set; }

        public List<string> Options { get; set; } = new List<string>();

        public List<string> DefaultSelectedOptions { get; set; } = new List<string>();

        public int MaximumSelectionLength { get; set; }

        public List<FieldDto> CascadeFields { get; set; } = new List<FieldDto>();

        public int Index { get; set; }

        public string NameId => Name + FieldStage;
    }
}
