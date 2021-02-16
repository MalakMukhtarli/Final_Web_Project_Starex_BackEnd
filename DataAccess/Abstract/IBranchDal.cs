using Core.Repository;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IBranchDal : IEntityRepository<Branch>
    {
    }
}
