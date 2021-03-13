using Core.Repository;
using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsDal: IEntityRepository<News>
    {
        Task<List<News>> GetAllOrder(Expression<Func<News, bool>> filter = null);
    }
}
