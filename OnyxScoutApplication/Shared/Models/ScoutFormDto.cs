using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models
{
    public class ScoutFormDto
    {
        public int Id { get; set; }

        public int TeamNumber { get; set; }

        public int Year { get; set; }

        public string MatchName { get; set; }

        public string WriterUserName { get; set; }

        public Dictionary<StageDto, List<ScoutFormDataDto>> DataByStages { get; set; } =
            new Dictionary<StageDto, List<ScoutFormDataDto>>();
    }
}
