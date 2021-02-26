using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Balancess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFBalanceDal: EFEntityRepositoryBase<Balance, AppDbContext>, IBalanceDal
    {
    }
}
