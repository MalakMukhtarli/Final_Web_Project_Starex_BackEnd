using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface ICityService
    {
        City GetCityWithId(int id);
        List<City> GetAllCity();
        void Add(City city);
        void Update(City city);
        void Delete(int id);
    }
}
