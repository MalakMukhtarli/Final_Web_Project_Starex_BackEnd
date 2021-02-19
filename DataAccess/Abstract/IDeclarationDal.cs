using Core.Repository;
using Entity.Entities.Declarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IDeclarationDal : IEntityRepository<Declaration>
    {
    }
}
