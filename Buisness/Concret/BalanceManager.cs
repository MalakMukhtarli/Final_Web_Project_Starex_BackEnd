using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Balancess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class BalanceManager : IBalanceService
    {
        private readonly IBalanceDal _balanceDal;

        public BalanceManager(IBalanceDal balanceDal)
        {
            _balanceDal = balanceDal;
        }
        async Task IBalanceService.Add(Balance data)
        {
            await _balanceDal.Add(data);
        }

        async Task IBalanceService.Delete(int id)
        {
            await _balanceDal.Delete(new Balance { Id = id });
        }

        public async Task<List<Balance>> GetAll()
        {
            return await _balanceDal.GetAll();
        }

        public async Task<Balance> GetWithId(int id)
        {
            return await _balanceDal.Get(b=>b.Id == id);
        }

        async Task IBalanceService.Update(Balance data)
        {
            await _balanceDal.Update(data);
        }
    }
}
