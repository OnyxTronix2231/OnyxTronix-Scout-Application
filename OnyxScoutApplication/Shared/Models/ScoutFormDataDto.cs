using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormData
    {
        [Key]
        public int Id { get; set; }

        public string Value {get; set; }

        [ForeignKey("Field")]
        public int FieldID { get; set; }

        public Field Field { get; set; }
    }
}
