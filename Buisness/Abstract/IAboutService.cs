using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IAboutService
    {
        Task<About> GetWithId(int id);
        Task<List<About>> GetAll();
        Task Add(About data);
        Task Update(About data);
        Task Delete(int id);
    }
}
