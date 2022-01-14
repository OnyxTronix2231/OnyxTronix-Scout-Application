using System.Collections.Generic;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    public class Form
    {
        public int Id { get; set; }

        public int TeamNumber { get; set; }

        public int Year { get; set; }

        public string MatchName { get; set; }

        public string WriterUserName { get; set; }

        public List<FormDataInStage> FormDataInStages { get; set; } = new List<FormDataInStage>();
    }
}
