using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class CountryContactManager : ICountryContactService
    {
        private readonly ICountryContactDal _countryContactDal;
        public CountryContactManager(ICountryContactDal countryContactDal)
        {
            _countryContactDal = countryContactDal;
        }
        public void Add(CountryContact contact)
        {
            _countryContactDal.Add(contact);
        }

        public void Delete(int id)
        {
            _countryContactDal.Delete(new CountryContact { Id = id });
        }

        public List<CountryContact> GetAllContact()
        {
            return _countryContactDal.GetAll(cc => !cc.IsDeleted);
        }

        public CountryContact GetContactWithId(int id)
        {
            return _countryContactDal.Get(cc => cc.Id == id && !cc.IsDeleted);
        }

        public void Update(CountryContact contact)
        {
            _countryContactDal.Update(contact);
        }
    }
}
