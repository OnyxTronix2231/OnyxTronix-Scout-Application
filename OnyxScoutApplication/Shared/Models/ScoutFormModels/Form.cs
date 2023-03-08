using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.ScoutFormModels
{
    [FirestoreData]
    public class Form
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        
        [FirestoreDocumentUpdateTimestamp]
        public DateTimeOffset DateTime { get; set; }

        [FirestoreProperty]
        public int TeamNumber { get; set; }

        [FirestoreProperty]
        public int Year { get; set; }
        
        [FirestoreProperty]
        public bool ImageRequired { get; set; }

        [FirestoreProperty]
        public ScoutFormType Type { get; set; }
        
        [FirestoreProperty]
        public string KeyName { get; set; }
        
        [FirestoreProperty]
        public string EventName { get; set; }
        
        [FirestoreProperty]
        public string WriterUserName { get; set; }

        [FirestoreProperty]
        public List<FormDataInStage> FormDataInStages { get; set; } = new();
        
        [FirestoreProperty]
        public bool IsImageUploaded { get; set; }
        
        [FirestoreProperty]
        public string ImageName { get; set; }
        
        [FirestoreProperty]
        public string ImageFileName { get; set; }
    }
}
