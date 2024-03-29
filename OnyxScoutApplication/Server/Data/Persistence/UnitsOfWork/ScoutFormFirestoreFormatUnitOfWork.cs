﻿using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;
using OnyxScoutApplication.Server.Data.Persistence.Repositories;
using AutoMapper;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork.interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.UnitsOfWork
{
    public class ScoutFormFaunaFormatUnitOfWork : IScoutFormFormatUnitOfWork
    {

        public ScoutFormFaunaFormatUnitOfWork(FirestoreDb client, IMapper mapper)
        {
            ScoutFormFormats = new ScoutFormFormatFirestoreRepository(client, mapper);
        }

        public IScoutFormFormatRepository ScoutFormFormats { get; }

        public void Dispose()
        {
        }
    }
}
