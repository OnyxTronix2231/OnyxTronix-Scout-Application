using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDto
    {
        public int Id { get; set; }

        public int TeamNumber { get; set; }

        public int Year { get; set; }

        public string MatchName { get; set; }

        public string WriterUserName { get; set; }

        public List<FormDataInStageDto> FormDataInStages { get; set; } = new List<FormDataInStageDto>();
    }
}
