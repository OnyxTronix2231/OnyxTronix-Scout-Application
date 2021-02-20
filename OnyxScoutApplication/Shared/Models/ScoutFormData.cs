using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormData
    {
        public int Id { get; set; }

        public int ScoutFormId { get; set; }

        public int? ScoutFormDataId { get; set; }

        [ForeignKey("Field")]
        public int FieldId { get; set; }

        public Field Field { get; set; }

        public string Value { get; set; }

        public List<ScoutFormData> CascadeData { get; set; } = new List<ScoutFormData>();
    }
}
