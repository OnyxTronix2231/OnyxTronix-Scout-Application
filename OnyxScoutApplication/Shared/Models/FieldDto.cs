﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace OnyxScoutApplication.Shared.Models
{
    public class FieldDto
    {
        public int Id { get; set; }

        //[ForeignKey("ScoutFormForamt")]
        public int ScoutFormForamtId { get; set; }

        [JsonIgnore]
        public ScoutFormFormat ScoutFormForamt { get; set; }

        public string Name { get; set; }

        public string TextDefaultValue { get; set; }

        public bool BoolDefaultValue { get; set; }

        public int? NumricDefaultValue { get; set; }

        public bool CascadeConditionDefaultValue { get; set; }

        public int MyProperty { get; set; }

        public FieldType FieldType { get; set; }

        public FieldStageType FieldStageType { get; set; }

        public int MaxValue { get; set; } = 9999;

        public int MinValue { get; set; } = 0;

        public bool Required { get; set; } = false;

        public List<string> Options { get; set; } = new List<string>();

        public List<string> DefaultSelectedOptions { get; set; } = new List<string>();

        public int MaximumSelectionLength { get; set; }

        public List<FieldDto> CascadeFields { get; set; } = new List<FieldDto>();

        public string NameId => Name + FieldStageType.ToString();
    }
}
