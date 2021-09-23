using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lanthanum.Data;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum.Web.Data.Repositories
{
    public class DbRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationContext Context;
        public DbRepository(ApplicationContext context)
        {
            Context = context;
        }
        
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        public DbSet<TEntity> GetEntity()
        {
            return Context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
        
        public async Task AddAsync(TEntity entity)
        {
            if (entity.Id != default)
                throw new ArgumentException("Use default value for id property when add item");
            
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity.Id == default)
                throw new ArgumentException("Can not use default id value when you edit item");

            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}