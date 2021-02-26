using Entity.Entities.Address;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IAddressService
    {
        Task<Address> GetWithId(int id);
        Task<List<Address>> GetAll();
        Task Add(Address data);
        Task Update(Address data);
        Task Delete(int id);
    }
}
