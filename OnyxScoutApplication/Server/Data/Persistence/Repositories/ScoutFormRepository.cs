using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Api;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormRepository : FirestoreRepository<Form, FormDto>, IScoutFormRepository
    {
        public ScoutFormRepository(FirestoreDb  client, IMapper mapper) : base( client, mapper, "ScoutForms")
        {
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByType(ScoutFormType scoutFormType)
        {
            var forms = await CollectionReference.WhereEqualTo("Type", scoutFormType).GetSnapshotAsync();
            return Mapper.Map<List<FormDto>>(forms.Select(i => i.ConvertTo<Form>()));
        }
        
        public override async Task<ActionResult> Add(FormDto form)
        {
            var result = await CollectionReference.WhereEqualTo("Year", form.Year)
                .WhereEqualTo("Year", form.Year)
                .WhereEqualTo("KeyName", form.KeyName).WhereEqualTo("TeamNumber", form.TeamNumber).GetSnapshotAsync();
            
            if (result.Count != 0)
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form already exists!");
            }

            return await base.Add(form);
        }

        public async Task<ActionResult<FormDto>> GetWithFields(string id)
        {
            return await Get(id);
        }

        public async Task<ActionResult> Update(string id, FormDto scoutFormDto)
        {
            DocumentReference docRef = CollectionReference.Document(id);
            await docRef.SetAsync(Mapper.Map<Form>(scoutFormDto));
            return await Task.Run(() => new OkResult());
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await CollectionReference.WhereEqualTo("EventName", eventKey)
                .WhereEqualTo("Type", scoutFormType).GetSnapshotAsync();
            var v = Mapper.Map<List<FormDto>>(scoutForm.Select(i => i.ConvertTo<Form>()));
            return v;
        }

        public async Task<ActionResult<FormDto>> GetByTeamAndKey(int teamNumber, string key,
            ScoutFormType scoutFormType)
        {
            var scoutForms = await CollectionReference.WhereEqualTo("TeamNumber", teamNumber)
                .WhereEqualTo("EventName", key)
                .WhereEqualTo("Type", scoutFormType).GetSnapshotAsync();
            if (scoutForms.Count == 0)
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest, "This scout form doe's not exist!");
            }

            Debug.Assert(scoutForms.Count == 1);
                
            return Mapper.Map<FormDto>(scoutForms[0].ConvertTo<Form>());
        }
        
        public async Task<ActionResult<IEnumerable<SimpleFormDto>>> GetAllByEvent(string eventKey,
            ScoutFormType scoutFormType)
        {
            var scoutForm = await CollectionReference.WhereEqualTo("EventName", eventKey)
                .WhereEqualTo("Type", scoutFormType).GetSnapshotAsync();
            return Mapper.Map<List<SimpleFormDto>>(scoutForm.Select(i => i.ConvertTo<Form>()));
        }

        public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey,
            ScoutFormType scoutFormType)
        {
            
            var scoutForms = await CollectionReference.WhereEqualTo("TeamNumber", teamNumber)
                .WhereEqualTo("EventName", eventKey)
                .WhereEqualTo("Type", scoutFormType).GetSnapshotAsync();
            
            return Mapper.Map<List<FormDto>>(scoutForms.Select(i => i.ConvertTo<Form>()));
        }
    }
}
