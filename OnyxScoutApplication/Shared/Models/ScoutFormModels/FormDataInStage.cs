using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    [FirestoreData]
    public class FormDataInStage
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Index { get; set; }
        [FirestoreProperty]
        public string FormId { get; set; }
        [FirestoreProperty]
        public Form Form { get; set; }
        [FirestoreProperty]
        public List<FormData> FormData { get; set; } = new List<FormData>();
    }
}
