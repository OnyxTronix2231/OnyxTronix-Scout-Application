﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormDataDto
    {
        [Key]
        public int Id { get; set; }

        public string StringValue {get; set; }

        public int? NumricValue {get; set; }

        public bool BooleanValue {get; set; }

        [ForeignKey("Field")]
        public int FieldID { get; set; }

        public FieldDto Field { get; set; }
    }
}