using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Address;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class AddressManager : IAddressService
    {

        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        async Task IAddressService.Add(Address data)
        {
            await _addressDal.Add(data);
        }

        async Task IAddressService.Delete(int id)
        {
            await _addressDal.Delete(new Address { Id = id });
        }

        public async Task<List<Address>> GetAll()
        {
            return await _addressDal.GetAll();
        }

        public async Task<Address> GetWithId(int id)
        {
            return await _addressDal.Get(a => a.Id == id);
        }

        async Task IAddressService.Update(Address data)
        {
            await _addressDal.Update(data);
        }
    }
}
