using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public enum FieldType
    {
        None,
        Boolean,
        TextField,
        Numeric,
        CascadeField,
        OptionSelect,
        MultipleChoice,
        Timer
    }

    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TextDefaultValue { get; set; }

        public bool BoolDefaultValue { get; set; }

        public int? NumericDefaultValue { get; set; }

        public bool CascadeConditionDefaultValue { get; set; }

        public FieldType FieldType { get; set; }

        [ForeignKey("FieldsInStage")]
        public int? FieldStageId { get; set; }
        
        public FieldsInStage FieldsInStage { get; set; }

        public int MaxValue { get; set; } = 9999;

        public int MinValue { get; set; }

        public bool Required { get; set; }

        public string Options { get; set; } = "";

        public int MaximumSelectionLength { get; set; }

        public List<Field> CascadeFields { get; set; }

        public int Index { get; set; }
    }
}
