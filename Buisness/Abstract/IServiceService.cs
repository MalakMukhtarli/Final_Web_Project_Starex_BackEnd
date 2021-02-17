using Entity.Entities.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IServiceService
    {
        Task<Service> GetWithId(int id);
        Task<List<Service>> GetAll();
        Task Add(Service service);
        Task Update(Service service);
        Task Delete(int id);
    }
}
