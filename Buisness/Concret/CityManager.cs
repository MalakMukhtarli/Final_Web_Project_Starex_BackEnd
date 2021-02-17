using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        public async Task<List<City>> GetAll()
        {
            return await _cityDal.GetAll(c => !c.IsDeleted);
        }

        public async Task<City> GetWithId(int id)
        {
            return await _cityDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        async Task ICityService.Add(City city)
        {
            await _cityDal.Add(city);
        }

        async Task ICityService.Delete(int id)
        {
            await _cityDal.Delete(new City { Id = id });
        }

        async Task ICityService.Update(City city)
        {
            await _cityDal.Update(city);
        }
    }
}
