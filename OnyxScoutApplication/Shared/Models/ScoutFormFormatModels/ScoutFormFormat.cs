using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class ScoutFormFormat
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public List<FieldsInStage> FieldsInStages { get; set; } = new List<FieldsInStage>();
    }
}
