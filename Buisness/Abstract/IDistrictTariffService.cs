using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface IDistrictTariffService
    {
        DistrictTariff GetTariffWithId(int id);
        List<DistrictTariff> GetAllTariff();
        void Add(DistrictTariff tariff);
        void Update(DistrictTariff tariff);
        void Delete(int id);
    }
}
