using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface INewsDetailService
    {
        Task<NewsDetail> GetWithId(int id);
        Task<List<NewsDetail>> GetAll();
        Task Add(NewsDetail data);
        Task Update(NewsDetail data);
        Task Delete(int id);
    }
}
