using Core.Repository;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICountryContactDal:IEntityRepository<CountryContact>
    {
    }
}
