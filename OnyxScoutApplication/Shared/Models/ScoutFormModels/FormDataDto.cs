﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataDto
    {
        public int Id { get; set; }
        
        public int? FieldId { get; set; }

        public FieldDto Field { get; set; }

        public int? FormDataStageId { get; set; }
        
        public FormDataInStageDto FormDataInStage { get; set; }

        public string StringValue { get; set; }

        public int? NumericValue { get; set; }

        public bool BooleanValue { get; set; }
        
        public TimeSpan? TimeSpanValue { get; set; }

        public List<string> SelectedOptions { get; set; } = new List<string>();

        public List<FormDataDto> CascadeData { get; set; } = new List<FormDataDto>();
    }
}