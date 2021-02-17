using Entity.Entities.Newss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface INewsService
    {
        Task<News> GetWithId(int id);
        Task<List<News>> GetAll();
        Task Add(News data);
        Task Update(News data);
        Task Delete(int id);
    }
}
