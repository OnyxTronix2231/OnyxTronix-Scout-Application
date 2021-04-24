using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class FormDataInStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public int? ScoutFormId { get; set; }
        public Form Form { get; set; }
        public List<FormDataDto> FormData { get; set; } = new List<FormDataDto>();
    }
}
