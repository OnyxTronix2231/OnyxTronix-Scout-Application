using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using static OnyxScoutApplication.Server.Data.Extensions.Result;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories;

public class ScoutFormFormatFirestoreRepositorySmart : FirestoreRepository<Form, FormDto>, IScoutFormRepository
{
    // private List<Form> formDtos;
    // private bool isInit;
    private Dictionary<string, List<Form>> formsByEventKey;

    public ScoutFormFormatFirestoreRepositorySmart(FirestoreDb client, IMapper mapper) : base(client, mapper,
        "ScoutForms")
    {
        // isInit = false;
        formsByEventKey = new Dictionary<string, List<Form>>();
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
        // await Init();
        // return Mapper.Map<FormDto>(formDtos.Find(s => s.Id == id));
        return Mapper.Map<FormDto>(await Get(id));
    }

    public async Task<ActionResult> Update(string id, FormDto scoutFormDto)
    {
        // await Init();
        DocumentReference docRef = CollectionReference.Document(id);
        await docRef.SetAsync(Mapper.Map<Form>(scoutFormDto));
        return await Task.Run(() => new OkResult());
    }

    public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByTeamWithData(int teamNumber, string eventKey,
        ScoutFormType scoutFormType)
    {
        await Init(eventKey);
        return Mapper.Map<List<FormDto>>(formsByEventKey[eventKey].FindAll(s => s.TeamNumber == teamNumber &&
            s.EventName == eventKey &&
            s.Type == scoutFormType));
    }

    public async Task<ActionResult<IEnumerable<SimpleFormDto>>> GetAllByEvent(string eventKey,
        ScoutFormType scoutFormType)
    {
        await Init(eventKey);
        return Mapper.Map<List<SimpleFormDto>>(formsByEventKey[eventKey]
            .FindAll(s => s.EventName == eventKey && s.Type == scoutFormType));
    }

    public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByEventWithData(string eventKey,
        ScoutFormType scoutFormType)
    {
        await Init(eventKey);
        return Mapper.Map<List<FormDto>>(formsByEventKey[eventKey]
            .FindAll(s => s.EventName == eventKey && s.Type == scoutFormType));
    }

    public async Task<ActionResult<IEnumerable<FormDto>>> GetAllByType(ScoutFormType scoutFormType)
    {
        // await Init();
        // return Mapper.Map<List<FormDto>>(formsByEventKey[eventKey].FindAll(s => s.Type == scoutFormType));
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<ActionResult<FormDto>> GetByTeamAndKey(int teamNumber, string key, ScoutFormType scoutFormType)
    {
        await Init(key);
        return Mapper.Map<FormDto>(formsByEventKey[key]
            .Find(s => s.TeamNumber == teamNumber && s.EventName == key && s.Type == scoutFormType));
    }

    private async Task Init(string eventKey)
    {
        if (formsByEventKey.ContainsKey(eventKey))
        {
            return;
        }

        var collection = CollectionReference.WhereEqualTo("EventName", eventKey);
        
        var forms = await collection.GetSnapshotAsync();
        //formDtos = Mapper.Map<List<FormDto>>(forms.Select(i => i.ConvertTo<Form>()));
        formsByEventKey[eventKey] = forms.Select(i => i.ConvertTo<Form>()).ToList();
        collection.Listen(snapshot =>
        {
            Console.WriteLine($"New chage in {eventKey} ScoutForms, updating cache");
            formsByEventKey[eventKey] = snapshot.Select(i => i.ConvertTo<Form>()).ToList();
        });
    }
}
