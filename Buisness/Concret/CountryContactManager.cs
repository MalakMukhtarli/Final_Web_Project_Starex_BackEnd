using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class CountryContactManager : ICountryContactService
    {
        private readonly ICountryContactDal _countryContactDal;
        public CountryContactManager(ICountryContactDal countryContactDal)
        {
            _countryContactDal = countryContactDal;
        }

        public async Task<List<CountryContact>> GetAll()
        {
            return await _countryContactDal.GetAll(cc => !cc.IsDeleted);
        }

        public async Task<CountryContact> GetWithId(int id)
        {
            return await _countryContactDal.Get(cc => cc.Id == id && !cc.IsDeleted);
        }

        async Task ICountryContactService.Add(CountryContact contact)
        {
            await _countryContactDal.Add(contact);
        }

        async Task ICountryContactService.Delete(int id)
        {
            await _countryContactDal.Delete(new CountryContact { Id = id });
        }

        async Task ICountryContactService.Update(CountryContact contact)
        {
            await _countryContactDal.Update(contact);
        }
    }
}
