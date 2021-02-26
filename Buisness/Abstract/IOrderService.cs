using Entity.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IOrderService
    {
        Task<Order> GetWithId(int id);
        Task<List<Order>> GetAll();
        Task Add(Order data);
        Task Update(Order data);
        Task Delete(int id);
    }
}
