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
        protected FirestoreDb  Client;
        protected readonly string collectionName;
        protected IMapper Mapper { get; }

        protected FirestoreRepository(FirestoreDb  client, IMapper mapper, string collectionName)
        {
            Client = client;
            this.collectionName = collectionName;
            Mapper = mapper;
        }

        public virtual async Task<ActionResult<TDtoEntity>> Get(string id)
        {
            var value = Client.Collection(collectionName);
            var docRefs = new List<DocumentReference>
            {
                value.Document(id)
            };
            Query query = value.WhereIn(FieldPath.DocumentId, docRefs);
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
            var collRef = Client.Collection(collectionName);
            QuerySnapshot snapshot = await collRef.GetSnapshotAsync();
            var v = snapshot.Documents.Select(d => d.ConvertTo<TDbEntity>());
            return new OkObjectResult(Mapper.Map<IEnumerable<TDtoEntity>>(v));
        }

        public virtual async Task<ActionResult> Add(TDtoEntity form)
        {
            var mapped = Mapper.Map<TDbEntity>(form);

            DocumentReference docRef = Client.Collection(collectionName).Document();
           
            await docRef.SetAsync(mapped);
            return await Task.Run(() => new OkResult());
        }

        public virtual async Task<ActionResult> Remove(string id)
        {
            // var entity = await Context.Set<TDbEntity>().FindAsync(id.ToString());
            // if (entity == null)
            // {
            //     return new NotFoundResult();
            // }
            //
            // Context.Set<TDbEntity>().Remove(Mapper.Map<TDbEntity>(entity));
            //return new OkResult();
            throw new NotImplementedException();
        }

        public virtual async Task UpdateFromTracking(TDtoEntity obj)
        {
           // Context.Entry(obj).State = EntityState.Modified;
          //  await Context.SaveChangesAsync();
        }
    }
}
