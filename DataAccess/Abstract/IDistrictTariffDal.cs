using Core.Repository;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IDistrictTariffDal : IEntityRepository<DistrictTariff>
    {
    }
}
