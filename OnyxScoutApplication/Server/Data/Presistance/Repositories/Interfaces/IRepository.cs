using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<ActionResult<TEntity>> Get(int id);

        Task<ActionResult<IEnumerable<TEntity>>> GetAll();

        Task<ActionResult<IEnumerable<TEntity>>> Find(Expression<Func<TEntity,bool>> predicate);

        Task<ActionResult> Add(TEntity entity);

        Task<ActionResult> Remove(int id);
    }
}
