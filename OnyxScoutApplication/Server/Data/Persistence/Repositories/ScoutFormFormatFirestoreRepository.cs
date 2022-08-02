using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using OnyxScoutApplication.Server.Data.Extensions;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class ScoutFormFormatFirestoreRepository : FirestoreRepository<ScoutFormFormat, ScoutFormFormatDto>, IScoutFormFormatRepository
    {
        public ScoutFormFormatFirestoreRepository(FirestoreDb  client, IMapper mapper) : base(client, mapper, "ScoutFormsFormat")
        {
        }

        public override async Task<ActionResult> Add(ScoutFormFormatDto form)
        {
            var q = CollectionReference.WhereEqualTo("Year", form.Year)
                .WhereEqualTo("ScoutFormType", form.ScoutFormType);
            var result = await q.GetSnapshotAsync();
            if (result.Count != 0)
            {
                return ResultCode(System.Net.HttpStatusCode.BadRequest,
                    "This scout format already exists for this year!");
            }

            GenerateFieldId(form);
            return await base.Add(form);
        }

        public async Task<ActionResult<FormDto>> GetTemplateScoutFormByYear(int year, ScoutFormType scoutFormType)
        {
            var result = await GetWithFieldsByYear(year, scoutFormType);
            if (result.Value == null)
            {
                return new NotFoundObjectResult("No scout form format found for year - " + year);
            }

            return Mapper.Map<FormDto>(result.Value);
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFields(string id)
        {
            var result = await Get(id);
            if (result.Value == null)
            {
                return result;
            }
            SortScoutFormFormat(result.Value);
            return result;
        }

        public async Task<ActionResult<ScoutFormFormatDto>> GetWithFieldsByYear(int year, ScoutFormType scoutFormType)
        {
            var q = CollectionReference.WhereEqualTo("Year", year).WhereEqualTo("ScoutFormType", scoutFormType);
            var v = await q.GetSnapshotAsync();
            if (v.Count == 0)
            {
                return new NotFoundObjectResult("No scout form format found for year: " + year);
            }

            var result = v[0].ConvertTo<ScoutFormFormat>();
            result.FieldsInStages = result.FieldsInStages.OrderBy(i => i.Index).ToList();

            
            var dto = Mapper.Map<ScoutFormFormatDto>(result);
            SortScoutFormFormat(dto);
            return dto;
        }

        public async Task<ActionResult> Update(string id, ScoutFormFormatDto scoutFormFormatDto)
        {
            GenerateFieldId(scoutFormFormatDto);
            DocumentReference docRef = CollectionReference.Document(id);
            await docRef.SetAsync(Mapper.Map<ScoutFormFormat>(scoutFormFormatDto));
            return await Task.Run(() => new OkResult());
        }

        private void GenerateFieldId(ScoutFormFormatDto scoutFormFormat)
        {
            foreach (var stage in scoutFormFormat.FieldsInStages)
            {
                foreach (var field in stage.Fields)
                {
                    field.Id = field.Id == Guid.Empty ? Guid.NewGuid(): field.Id;
                }
            }
        }

        private void SortScoutFormFormat(ScoutFormFormatDto scoutFormFormat)
        {
            scoutFormFormat.FieldsInStages.Sort((i1,i2) => i1.Index.CompareTo(i2.Index));
            foreach (var stage in scoutFormFormat.FieldsInStages)
            {
                SortForEachField(stage.Fields);
            }
        }
        
        private void SortForEachField(List<FieldDto> fields)
        {
            fields.Sort((i1,i2) => i1.Index.CompareTo(i2.Index));
            foreach (var field in fields)
            {
                field.Options.Sort((i1,i2) => i1.Index.CompareTo(i2.Index));
                SortForEachField(field.CascadeFields);
            }
        }
    }
}
