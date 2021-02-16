using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface ITariffService
    {
        Tariff GetTariffWithId(int id);
        List<Tariff> GetAllTariff();
        void Add(Tariff tariff);
        void Update(Tariff tariff);
        void Delete(int id);
    }
}
