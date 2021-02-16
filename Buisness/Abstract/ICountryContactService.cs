using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface ICountryContactService
    {
        CountryContact GetContactWithId(int id);
        List<CountryContact> GetAllContact();
        void Add(CountryContact contact);
        void Update(CountryContact contact);
        void Delete(int id);
    }
}
