using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormFormatDto
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public List<FieldsInStageDto> FieldsInStages { get; set; } = new List<FieldsInStageDto>();
    }
}
