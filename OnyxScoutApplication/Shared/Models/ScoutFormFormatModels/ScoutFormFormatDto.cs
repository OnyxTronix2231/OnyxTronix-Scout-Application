using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class ScoutFormFormatDto
    {
        public int Id { get; set; }

        public int Year { get; set; }
        
        public ScoutFormType ScoutFormType { get; set; }

        public List<FieldsInStageDto> FieldsInStages { get; set; } = new List<FieldsInStageDto>();
    }
}
