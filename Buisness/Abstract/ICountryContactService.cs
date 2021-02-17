using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ICountryContactService
    {
        Task<CountryContact> GetWithId(int id);
        Task<List<CountryContact>> GetAll();
        Task Add(CountryContact contact);
        Task Update(CountryContact contact);
        Task Delete(int id);
    }
}
