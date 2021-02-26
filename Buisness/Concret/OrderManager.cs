using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        async Task IOrderService.Add(Order data)
        {
           await _orderDal.Add(data);
        }

        async Task IOrderService.Delete(int id)
        {
           await _orderDal.Delete(new Order { Id = id });
        }

        public async Task<List<Order>> GetAll()
        {
            return await _orderDal.GetAll(o => !o.IsDeleted);
        }

        public async Task<Order> GetWithId(int id)
        {
            return await _orderDal.Get(o => o.Id == id && !o.IsDeleted);
        }

        async Task IOrderService.Update(Order data)
        {
           await _orderDal.Update(data);
        }
    }
}
