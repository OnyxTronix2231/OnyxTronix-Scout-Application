using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using Google.Cloud.Firestore;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class FirestoreRepository<TDbEntity, TDtoEntity> : IRepository<TDtoEntity>
        where TDbEntity : class where TDtoEntity : class
    {
        protected readonly CollectionReference CollectionReference;
        protected IMapper Mapper { get; }

        protected FirestoreRepository(FirestoreDb  client, IMapper mapper, string collectionName)
        {
            CollectionReference = client.Collection(collectionName);
            Mapper = mapper;
        }

        public virtual async Task<ActionResult<TDtoEntity>> Get(string id)
        {
            Query query = CollectionReference.WhereEqualTo(FieldPath.DocumentId, id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            if (querySnapshot.Count == 0)
            {
                return new NotFoundObjectResult("No record found with the id of: " + id);
            }

            var e = querySnapshot.Documents[0].ConvertTo<TDbEntity>();
            return Mapper.Map<TDtoEntity>(e);
        }

        public virtual async Task<ActionResult<IEnumerable<TDtoEntity>>> GetAll()
        {
            QuerySnapshot snapshot = await CollectionReference.GetSnapshotAsync();
            var v = snapshot.Documents.Select(d => d.ConvertTo<TDbEntity>());
            return new OkObjectResult(Mapper.Map<IEnumerable<TDtoEntity>>(v));
        }

        public virtual async Task<ActionResult> Add(TDtoEntity form)
        {
            var mapped = Mapper.Map<TDbEntity>(form);

            DocumentReference docRef = CollectionReference.Document();
           
            await docRef.SetAsync(mapped);
            return new OkResult();
        }

        public virtual async Task<ActionResult> Remove(string id)
        {
            Query query = CollectionReference.WhereEqualTo(FieldPath.DocumentId, id);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            
            if (querySnapshot.Count == 0)
            {
                return new NotFoundObjectResult("No record found with the id of: " + id);
            }

            var res = await querySnapshot.Documents[0].Reference.DeleteAsync();
            return new OkObjectResult("Form removed successfully");
        }

        public Task UpdateFromTracking(TDtoEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
