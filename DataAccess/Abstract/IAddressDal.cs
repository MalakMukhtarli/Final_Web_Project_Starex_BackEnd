using Core.Repository;
using Entity.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAddressDal: IEntityRepository<Address>
    {
    }
}
