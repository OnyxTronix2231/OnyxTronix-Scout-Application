using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataDto
    {
        public string Id { get; set; }
        
        //public string FieldId { get; set; }

        public FieldDto Field { get; set; }

        public int? FormDataStageId { get; set; }
        
        public FormDataInStageDto FormDataInStage { get; set; }

        public string StringValue { get; set; }

        public float? NumericValue { get; set; }
        
        public bool BooleanValue { get; set; }
        
        public List<OptionDto> SelectedOptions { get; set; } = new();

        public List<FormDataDto> CascadeData { get; set; } = new();
    }
}
