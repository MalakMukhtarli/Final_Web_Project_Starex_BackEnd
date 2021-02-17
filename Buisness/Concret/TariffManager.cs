using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class TariffManager : ITariffService
    {
        private readonly ITariffDal _tariffDal;
        public TariffManager(ITariffDal tariffDal)
        {
            _tariffDal = tariffDal;
        }
        public async Task<List<Tariff>> GetAll()
        {
            return await _tariffDal.GetAll(t => !t.IsDeleted);
        }

        public async Task<Tariff> GetWithId(int id)
        {
            return await _tariffDal.Get(t => t.Id == id && !t.IsDeleted);
        }

        async Task ITariffService.Add(Tariff tariff)
        {
            await _tariffDal.Add(tariff);
        }

        async Task ITariffService.Delete(int id)
        {
            await _tariffDal.Delete(new Tariff { Id = id });
        }

        async Task ITariffService.Update(Tariff tariff)
        {
            await _tariffDal.Update(tariff);
        }
    }
}
