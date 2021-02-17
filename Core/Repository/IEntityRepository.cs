using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
