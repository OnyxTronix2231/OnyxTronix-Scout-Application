using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models
{
    public class FormDataInStageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public List<ScoutFormDataDto> Data { get; set; }
    }
}
