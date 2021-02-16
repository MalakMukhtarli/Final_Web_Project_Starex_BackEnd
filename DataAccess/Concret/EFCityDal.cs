using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFCityDal : EFEntityRepositoryBase<City,AppDbContext>, ICityDal
    {
    }
}
