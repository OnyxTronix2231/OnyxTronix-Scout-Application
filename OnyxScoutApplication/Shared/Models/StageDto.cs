using System;

namespace OnyxScoutApplication.Shared.Models
{
    public class StageDto : IComparable<StageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public int CompareTo(StageDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
}
