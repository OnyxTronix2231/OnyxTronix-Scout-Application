using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    [FirestoreData]
    public class ScoutFormFormat
    {
        public int Id { get; set; }

        [FirestoreProperty]
        public int Year { get; set; }
        
        [FirestoreProperty]
        public ScoutFormType ScoutFormType { get; set; }

        [FirestoreProperty]
        public List<FieldsInStage> FieldsInStages { get; set; } = new();
    }
}
