using System;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class GuidFirestoreConvertor : IFirestoreConverter<Guid>
    {
        public object ToFirestore(Guid value)
        {
            return value.ToString();
        }

        public Guid FromFirestore(object value)
        {
            return new Guid(value.ToString()!);
        }
    }
}
