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

namespace OnyxScoutApplication.Server.Data.Persistence.Repositories
{
    public class Repository<TDbEntity, TDtoEntity> : IRepository<TDtoEntity>
        where TDbEntity : class where TDtoEntity : class
    {
        protected DbContext Context { get; }
        protected IMapper Mapper { get; }

        protected Repository(DbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<ActionResult<TDtoEntity>> Get(int id)
        {
            var entity = await Context.Set<TDbEntity>().FindAsync(id);
            if (entity == null)
            {
                return new NotFoundResult();
            }

            return Mapper.Map<TDtoEntity>(entity);
        }

        public virtual async Task<ActionResult<IEnumerable<TDtoEntity>>> GetAll()
        {
            return new OkObjectResult(Mapper.Map<IEnumerable<TDtoEntity>>(await Context.Set<TDbEntity>().ToListAsync()));
        }

        public virtual async Task<ActionResult> Add(TDtoEntity form)
        {
            //DbEntity db = Activator.CreateInstance<DbEntity>();
            var mapped = Mapper.Map<TDbEntity>(form);
            await Context.Set<TDbEntity>().AddAsync(mapped);
            return await Task.Run(() => new OkResult());
        }

        //public virtual async Task<ActionResult<IEnumerable<DtoEntity>>> Find(Expression<Func<DtoEntity, bool>> predicate)
        //{
        //    return await context.Set<DbEntity>().Where(predicate).ToListAsync();
        //}

        public virtual async Task<ActionResult> Remove(int id)
        {
            var entity = await Context.Set<TDbEntity>().FindAsync(id.ToString());
            if (entity == null)
            {
                return new NotFoundResult();
            }

            Context.Set<TDbEntity>().Remove(Mapper.Map<TDbEntity>(entity));
            return new OkResult();
        }
    }
}
