using Entity.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IAdvantagesService
    {
        Task<Advantages> GetWithId(int id);
        Task<List<Advantages>> GetAll();
        Task Add(Advantages data);
        Task Update(Advantages data);
        Task Delete(int id);
    }
}
