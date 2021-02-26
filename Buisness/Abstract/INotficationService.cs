using Entity.Entities.Notfications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface INotficationService
    {
        Task<Notfication> GetWithId(int id);
        Task<List<Notfication>> GetAll();
        Task Add(Notfication data);
        Task Update(Notfication data);
        Task Delete(int id);
    }
}
