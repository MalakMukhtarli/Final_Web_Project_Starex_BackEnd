using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface ICountryService
    {
        Country GetCountryWithId(int id);
        List<Country> GetAllCountry();
        void Add(Country country);
        void Update(Country country);
        void Delete(int id);
    }
}
