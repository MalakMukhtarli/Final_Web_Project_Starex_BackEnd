using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ICityService
    {
        Task<City> GetWithId(int id);
        Task<List<City>> GetAll();
        Task Add(City city);
        Task Update(City city);
        Task Delete(int id);
    }
}
