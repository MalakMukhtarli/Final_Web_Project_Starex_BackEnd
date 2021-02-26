using Entity.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IStoreService
    {
        Task<Store> GetWithId(int id);
        Task<List<Store>> GetAll();
        Task Add(Store data);
        Task Update(Store data);
        Task Delete(int id);
    }
}
