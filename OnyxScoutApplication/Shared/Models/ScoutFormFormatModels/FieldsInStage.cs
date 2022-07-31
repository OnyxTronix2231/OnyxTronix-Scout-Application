using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    [FirestoreData]
    public class FieldsInStage
    {
        public int Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Index { get; set; }
        [ForeignKey("ScoutFormFormat")]
        public int ScoutFormFormatId { get; set; }
        public ScoutFormFormat ScoutFormFormat { get; set; }
        public List<Field> Fields { get; set; }
    }
}
