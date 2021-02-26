using Core.Repository;
using Entity.Entities.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAddressDal: IEntityRepository<Address>
    {
    }
}
