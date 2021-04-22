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

        public Dictionary<string, List<FieldDto>> FieldsByStages { get; set; } = new Dictionary<string, List<FieldDto>>();
    }
}
