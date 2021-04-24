using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormFormatDto
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public SortedDictionary<StageDto, List<FieldDto>> FieldsByStages { get; set; } = new SortedDictionary<StageDto, List<FieldDto>>();
    }
}
