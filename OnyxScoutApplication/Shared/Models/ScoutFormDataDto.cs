using System;
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

        public int ScoutFormId { get; set; }

        public int? ScoutFormDataId { get; set; }

        [ForeignKey("Field")]
        public int FieldId { get; set; }

        public FieldDto Field { get; set; }

        public string StringValue {get; set; }

        public int? NumericValue {get; set; }

        public bool BooleanValue {get; set; }

        public List<string> SelectedOptions { get; set; } = new List<string>();

        public List<ScoutFormDataDto> CascadeData { get; set; } = new List<ScoutFormDataDto>();
    }
}
