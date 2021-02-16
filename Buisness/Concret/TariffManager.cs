using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class TariffManager : ITariffService
    {
        private readonly ITariffDal _tariffDal;
        public TariffManager(ITariffDal tariffDal)
        {
            _tariffDal = tariffDal;
        }
        public void Add(Tariff tariff)
        {
            _tariffDal.Add(tariff);
        }

        public void Delete(int id)
        {
            _tariffDal.Delete(new Tariff { Id = id });
        }

        public List<Tariff> GetAllTariff()
        {
            return _tariffDal.GetAll(t => !t.IsDeleted);
        }

        public Tariff GetTariffWithId(int id)
        {
            return _tariffDal.Get(t => t.Id == id && !t.IsDeleted);
        }

        public void Update(Tariff tariff)
        {
            _tariffDal.Update(tariff);
        }
    }
}
