using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class StoreManager : IStoreService
    {
        private readonly IStoreDal _storeDal;

        public StoreManager(IStoreDal storeDal)
        {
            _storeDal = storeDal;
        }

        async Task IStoreService.Add(Store data)
        {
            await _storeDal.Add(data);
        }

        async Task IStoreService.Delete(int id)
        {
            await _storeDal.Delete(new Store { Id = id });
        }

        public async Task<List<Store>> GetAll()
        {
            return await _storeDal.GetAll(s => !s.IsDeleted);
        }

        public async Task<Store> GetWithId(int id)
        {
            return await _storeDal.Get(s => s.Id == id && s.IsDeleted);
        }

        async Task IStoreService.Update(Store data)
        {
            await _storeDal.Update(data);
        }
    }
}
