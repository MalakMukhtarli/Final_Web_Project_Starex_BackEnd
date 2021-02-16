using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;
        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public void Add(City city)
        {
            _cityDal.Add(city);
        }

        public void Delete(int id)
        {
            _cityDal.Delete(new City { Id = id });
        }

        public List<City> GetAllCity()
        {
            return _cityDal.GetAll(c => !c.IsDeleted);
        }

        public City GetCityWithId(int id)
        {
            return _cityDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        public void Update(City city)
        {
            _cityDal.Update(city);
        }
    }
}
