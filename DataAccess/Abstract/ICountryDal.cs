using Core.Repository;
using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICountryDal : IEntityRepository<Country>
    {
    }
}
