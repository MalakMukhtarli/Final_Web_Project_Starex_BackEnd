using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFTariffDal: EFEntityRepositoryBase<Tariff, AppDbContext>, ITariffDal
    {
    }
}
