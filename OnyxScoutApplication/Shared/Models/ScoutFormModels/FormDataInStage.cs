using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataInStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        [ForeignKey("Form")]
        public int? FormId { get; set; }
        public Form Form { get; set; }
        public List<FormData> FormData { get; set; } = new List<FormData>();
    }
}
