using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public enum FieldType
    {
        None,
        Boolean,
        TextField,
        Integer,
        CascadeField,
        OptionSelect,
        MultipleChoice,
        Timer
    }

    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public FieldType FieldType { get; set; }

        [ForeignKey("FieldsInStage")]
        public int? FieldStageId { get; set; }
        
        [ForeignKey("ParentField")]
        public int? FieldId { get; set; }
        
        public FieldsInStage FieldsInStage { get; set; }
        
        public Field ParentField { get; set; }

        public int MaxValue { get; set; } = 9999;

        public int MinValue { get; set; }

        public bool Required { get; set; }
        
        public bool AllowManualInput { get; set; }

        public List<Option> Options { get; set; }

        public int MaximumSelectionLength { get; set; }

        public List<Field> CascadeFields { get; set; }

        public int Index { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public float PercentWeight { get; set; }
        public int FieldId { get; set; }
    }

}
