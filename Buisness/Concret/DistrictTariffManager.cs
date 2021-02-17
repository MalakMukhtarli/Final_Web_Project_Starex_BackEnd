using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class DistrictTariffManager : IDistrictTariffService
    {
        private readonly IDistrictTariffDal _districtTariffDal;
        public DistrictTariffManager(IDistrictTariffDal districtTariffDal)
        {
            _districtTariffDal = districtTariffDal;
        }

        public async Task<List<DistrictTariff>> GetAll()
        {
            return await _districtTariffDal.GetAll(t => !t.IsDeleted);
        }

        public async Task<DistrictTariff> GetWithId(int id)
        {
            return await _districtTariffDal.Get(t => t.Id == id && !t.IsDeleted);
        }

        async Task IDistrictTariffService.Add(DistrictTariff tariff)
        {
            await _districtTariffDal.Add(tariff);
        }

        async Task IDistrictTariffService.Delete(int id)
        {
            await _districtTariffDal.Delete(new DistrictTariff { Id = id });
        }

        async Task IDistrictTariffService.Update(DistrictTariff tariff)
        {
            await _districtTariffDal.Update(tariff);
        }
    }
}
