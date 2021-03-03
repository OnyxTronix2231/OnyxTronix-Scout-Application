using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnyxScoutApplication.Server.Data.Persistence.Repositories.Interfaces;

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class Repository<DbEntity, DtoEntity> : IRepository<DbEntity, DtoEntity>
        where DbEntity : class where DtoEntity : class
    {
        protected readonly DbContext context;
        protected readonly IMapper mapper;

        public Repository(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual async Task<ActionResult<DtoEntity>> Get(int id)
        {
            var entity = await context.Set<DbEntity>().FindAsync(id);
            if (entity == null)
            {
                return new NotFoundResult();
            }

            return mapper.Map<DtoEntity>(entity);
        }

        public virtual async Task<ActionResult<IEnumerable<DtoEntity>>> GetAll()
        {
            return new OkObjectResult(mapper.Map<IEnumerable<DtoEntity>>(await context.Set<DbEntity>().ToListAsync()));
        }

        public virtual async Task<ActionResult> Add(DtoEntity entity)
        {
            //DbEntity db = Activator.CreateInstance<DbEntity>();
            var mapped = mapper.Map<DbEntity>(entity);
            context.Set<DbEntity>().Add(mapped);
            return await Task.Run(() => new OkResult());
        }

        //public virtual async Task<ActionResult<IEnumerable<DtoEntity>>> Find(Expression<Func<DtoEntity, bool>> predicate)
        //{
        //    return await context.Set<DbEntity>().Where(predicate).ToListAsync();
        //}

        public virtual async Task<ActionResult> Remove(int id)
        {
            var entity = await context.Set<DbEntity>().FindAsync(id);
            if (entity == null)
            {
                return new NotFoundResult();
            }

            context.Set<DbEntity>().Remove(mapper.Map<DbEntity>(entity));
            return new OkResult();
        }
    }
}
