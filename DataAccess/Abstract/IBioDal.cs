using Core.Repository;
using Entity.Entities.Bios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IBioDal : IEntityRepository<Bio>
    {
    }
}
