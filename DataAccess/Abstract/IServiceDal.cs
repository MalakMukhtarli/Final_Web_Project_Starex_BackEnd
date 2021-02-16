using Core.Repository;
using Entity.Entities.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IServiceDal : IEntityRepository<Service>
    {
    }
}
