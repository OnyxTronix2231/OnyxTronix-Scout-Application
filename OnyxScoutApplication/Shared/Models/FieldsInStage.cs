using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models
{
    public class FieldsInStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormFormatId { get; set; }
        public ScoutFormFormat ScoutFormFormat { get; set; }
        public List<Field> Fields { get; set; }
    }
}
