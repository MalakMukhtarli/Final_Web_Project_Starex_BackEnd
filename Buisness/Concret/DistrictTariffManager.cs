using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class DistrictTariffManager : IDistrictTariffService
    {
        private readonly IDistrictTariffDal _districtTariffDal;
        public DistrictTariffManager(IDistrictTariffDal districtTariffDal)
        {
            _districtTariffDal = districtTariffDal;
        }
        public void Add(DistrictTariff tariff)
        {
            _districtTariffDal.Add(tariff);
        }

        public void Delete(int id)
        {
            _districtTariffDal.Delete(new DistrictTariff { Id = id });
        }

        public List<DistrictTariff> GetAllTariff()
        {
            return _districtTariffDal.GetAll(t => !t.IsDeleted);
        }

        public DistrictTariff GetTariffWithId(int id)
        {
            return _districtTariffDal.Get(t => t.Id == id && !t.IsDeleted);
        }

        public void Update(DistrictTariff tariff)
        {
            _districtTariffDal.Update(tariff);
        }
    }
}
