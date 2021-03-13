using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Newss;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concret
{
    public class EfNewsDal : EFEntityRepositoryBase<News, AppDbContext>, INewsDal
    {
        public async Task<List<News>> GetAllOrder(Expression<Func<News, bool>> filter = null)
        {
            using (var context = new AppDbContext())
            {
                return filter == null
                    ? await context.Set<News>().ToListAsync()
                    : await context.Set<News>().Where(filter).OrderByDescending(n=>n.Id).ToListAsync();
            }
        }
    }
}
