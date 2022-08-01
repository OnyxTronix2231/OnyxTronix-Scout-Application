using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    [FirestoreData]
    public class FormData
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string FieldId { get; set; }

        [FirestoreProperty]
        public Field Field { get; set; }
        
        [FirestoreProperty]
        public string FormDataStageId { get; set; }

        [FirestoreProperty]
        public FormDataInStage FormDataInStage { get; set; }

        [FirestoreProperty]
        public string Value { get; set; }

        [FirestoreProperty]
        public List<FormData> CascadeData { get; set; } = new();
    }
}
