using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Declarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFDeclarationDal : EFEntityRepositoryBase<Declaration, AppDbContext>, IDeclarationDal
    {
    }
}
