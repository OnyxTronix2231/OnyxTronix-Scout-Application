using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class FieldsInStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        [ForeignKey("ScoutFormFormat")]
        public int? ScoutFormFormatId { get; set; }
        public ScoutFormFormat ScoutFormFormat { get; set; }
        public List<Field> Fields { get; set; }
    }
}
