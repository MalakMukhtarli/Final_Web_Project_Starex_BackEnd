using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFOrderDal: EFEntityRepositoryBase<Order, AppDbContext>, IOrderDal
    {
    }
}
