using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ICountryService
    {
        Task<Country> GetWithId(int id);
        Task<List<Country>> GetAll();
        Task Add(Country country);
        Task Update(Country country);
        Task Delete(int id);
    }
}
