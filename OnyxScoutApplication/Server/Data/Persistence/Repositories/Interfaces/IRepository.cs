using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IRepository<TDtoEntity> where TDtoEntity : class
    {
        Task<ActionResult<TDtoEntity>> Get(int id);

        Task<ActionResult<IEnumerable<TDtoEntity>>> GetAll();

        Task<ActionResult> Add(TDtoEntity scoutFormFormat);

        Task<ActionResult> Remove(int id);
    }
}
