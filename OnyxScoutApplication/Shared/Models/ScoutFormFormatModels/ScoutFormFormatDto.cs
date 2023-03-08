using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class ScoutFormFormatDto
    {
        public string Id { get; set; }

        public int Year { get; set; }
        
        public bool ForceImageUpload { get; set; }
        
        public ScoutFormType ScoutFormType { get; set; }

        public List<FieldsInStageDto> FieldsInStages { get; set; } = new List<FieldsInStageDto>();
    }
}
