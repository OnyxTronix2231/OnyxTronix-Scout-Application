using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class ScoutFormData
    {
        public int Id { get; set; }

        public int ScoutFormId { get; set; }

        public int? ScoutFormDataId { get; set; }

        [ForeignKey("Field")]
        public int? FieldId { get; set; }

        public Field Field { get; set; }
        
        public int? FormDataStageId { get; set; }

        public FormDataInStage FormDataInStage { get; set; }

        public string Value { get; set; }

        public List<ScoutFormData> CascadeData { get; set; } = new List<ScoutFormData>();
    }
}
