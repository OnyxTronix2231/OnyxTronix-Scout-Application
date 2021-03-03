using Microsoft.AspNetCore.Mvc;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces
{
    public interface IRepository<DbEntity, DtoEntity> where DbEntity : class where DtoEntity : class
    {
        Task<ActionResult<DtoEntity>> Get(int id);

        Task<ActionResult<IEnumerable<DtoEntity>>> GetAll();

        //Task<ActionResult<IEnumerable<DtoEntity>>> Find(Expression<Func<DtoEntity, bool>> predicate);

        Task<ActionResult> Add(DtoEntity entity);

        Task<ActionResult> Remove(int id);
    }
}
