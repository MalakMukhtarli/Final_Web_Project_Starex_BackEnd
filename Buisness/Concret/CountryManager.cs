using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }
        public void Add(Country country)
        {
            _countryDal.Add(country);
        }

        public void Delete(int id)
        {
            _countryDal.Delete(new Country { Id = id });
        }

        public List<Country> GetAllCountry()
        {
            return _countryDal.GetAll(c => !c.IsDeleted);
        }

        public Country GetCountryWithId(int id)
        {
            return _countryDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        public void Update(Country country)
        {
            _countryDal.Update(country);
        }
    }
}
