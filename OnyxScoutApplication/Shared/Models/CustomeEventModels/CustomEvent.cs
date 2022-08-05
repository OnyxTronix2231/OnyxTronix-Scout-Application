using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Shared.Models.CustomeEventModels
{
    [FirestoreData]
    public class CustomEvent
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Country { get; set; }
        [FirestoreProperty]
        public string Key { get; set; }
        [FirestoreProperty]
        public int Year { get; set; }
        [FirestoreProperty]
        public DateTime StartDate { get; set; }
        [FirestoreProperty]
        public List<CustomMatch> Matches { get; set; } = new();
    }
}
