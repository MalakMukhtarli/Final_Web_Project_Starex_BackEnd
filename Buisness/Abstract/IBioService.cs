using Entity.Entities.Bios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IBioService
    {
        Task<Bio> GetWithId(int id);
        Task<List<Bio>> GetAll();
        Task Add(Bio data);
        Task Update(Bio data);
        Task Delete(int id);
    }
}
