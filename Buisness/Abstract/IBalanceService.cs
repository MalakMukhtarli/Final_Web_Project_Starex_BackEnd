using Entity.Entities.Balancess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IBalanceService
    {
        Task<Balance> GetWithId(int id);
        Task<List<Balance>> GetAll();
        Task Add(Balance data);
        Task Update(Balance data);
        Task Delete(int id);
    }
}
