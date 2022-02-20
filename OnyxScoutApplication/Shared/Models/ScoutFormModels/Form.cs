using System.Collections.Generic;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class Form
    {
        public int Id { get; set; }

        public int TeamNumber { get; set; }

        public int Year { get; set; }

        public ScoutFormType Type { get; set; }
        
        public string KeyName { get; set; }
        
        public string WriterUserName { get; set; }

        public List<FormDataInStage> FormDataInStages { get; set; } = new();
    }
}
