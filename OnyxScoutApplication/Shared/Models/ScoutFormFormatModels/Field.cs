using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Server.Data.Profiles.Resolvers;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public enum FieldType
    {
        Boolean,
        TextField,
        Integer,
        CascadeField,
        OptionSelect,
        MultipleChoice,
        Timer,
        BooleanChooser
    }

    [FirestoreData]
    public class Field
    {
        [FirestoreProperty(ConverterType = typeof(GuidFirestoreConvertor))]
        public Guid Id { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string DefaultValue { get; set; }

        [FirestoreProperty]
        public FieldType FieldType { get; set; }

        [FirestoreProperty]
        public int MaxValue { get; set; } = 9999;

        [FirestoreProperty]
        public int MinValue { get; set; }

        [FirestoreProperty]
        public bool Required { get; set; }
        
        [FirestoreProperty]
        public bool AllowManualInput { get; set; }

        [FirestoreProperty]
        public List<Option> Options { get; set; }

        [FirestoreProperty]
        public int MaximumSelectionLength { get; set; }

        [FirestoreProperty]
        public List<Field> CascadeFields { get; set; }

        [FirestoreProperty]
        public int Index { get; set; }
    }

    [FirestoreData]
    public class Option
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public int Index { get; set; }
        [FirestoreProperty]
        public float PercentWeight { get; set; }
        [FirestoreProperty]
        public int FieldId { get; set; }
    }

}
