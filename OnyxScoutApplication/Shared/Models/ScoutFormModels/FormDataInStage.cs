using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataInStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormId { get; set; }
        public ScoutForm ScoutForm { get; set; }
        public List<ScoutFormDataDto> FormData { get; set; } = new List<ScoutFormDataDto>();
    }
}
