using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.EFRepository
{
    public class EFEntityRepositoryBase<TEntity, IContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where IContext : DbContext,new()
    {

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new IContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().FirstOrDefaultAsync()
                    : await context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new IContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }
        async Task IEntityRepository<TEntity>.Add(TEntity entity)
        {
            using (var context = new IContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        async Task IEntityRepository<TEntity>.Update(TEntity entity)
        {
            using (var context = new IContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        async Task IEntityRepository<TEntity>.Delete(TEntity entity)
        {
            using (var context = new IContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
}
