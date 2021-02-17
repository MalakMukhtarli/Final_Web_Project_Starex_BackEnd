using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IDistrictTariffService
    {
        Task<DistrictTariff> GetWithId(int id);
        Task<List<DistrictTariff>> GetAll();
        Task Add(DistrictTariff tariff);
        Task Update(DistrictTariff tariff);
        Task Delete(int id);
    }
}
