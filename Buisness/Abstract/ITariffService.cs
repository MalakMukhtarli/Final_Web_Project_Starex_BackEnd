using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ITariffService
    {
        Task<Tariff> GetWithId(int id);
        Task<List<Tariff>> GetAll();
        Task Add(Tariff tariff);
        Task Update(Tariff tariff);
        Task Delete(int id);
    }
}
