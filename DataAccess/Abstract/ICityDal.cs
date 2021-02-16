using Core.Repository;
using Entity.Entities.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICityDal : IEntityRepository<City>
    {
    }
}
