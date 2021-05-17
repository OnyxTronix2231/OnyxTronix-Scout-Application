using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class FieldsInStageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormFormatId { get; set; }
        public ScoutFormFormatDto ScoutFormFormat { get; set; }
        public List<FieldDto> Fields { get; set; } = new List<FieldDto>();
        public bool IsCollapsed { get; set; }
        public bool IsNew { get; set; }

        public int CompareTo(FieldsInStageDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
