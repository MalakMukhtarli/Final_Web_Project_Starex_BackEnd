using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Countries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFCountryDal : EFEntityRepositoryBase<Country, AppDbContext>, ICountryDal
    {
    }
}
