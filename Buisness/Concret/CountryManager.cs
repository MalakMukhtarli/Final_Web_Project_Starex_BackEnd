using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public async Task<List<Country>> GetAll()
        {
            return await _countryDal.GetAll(c => !c.IsDeleted);
        }

        public async Task<Country> GetWithId(int id)
        {
            return await _countryDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        async Task ICountryService.Add(Country country)
        {
            await _countryDal.Add(country);
        }

        async Task ICountryService.Delete(int id)
        {
            await _countryDal.Delete(new Country { Id = id });
        }

        async Task ICountryService.Update(Country country)
        {
            await _countryDal.Update(country);
        }
    }
}
