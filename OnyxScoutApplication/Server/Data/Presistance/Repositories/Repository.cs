using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnyxScoutApplication.Server.Data.Presistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Presistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if(entity == null)
            {
                return new NotFoundResult();
            }
            return entity;
        }

        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<ActionResult> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return await Task.Run(() => new OkResult());
        }

        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ActionResult> Remove(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return new NotFoundResult();
            }
            context.Set<TEntity>().Remove(entity);
            return new OkResult();
        }
    }
}
