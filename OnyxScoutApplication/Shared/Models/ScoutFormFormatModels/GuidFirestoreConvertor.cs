using System;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
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
