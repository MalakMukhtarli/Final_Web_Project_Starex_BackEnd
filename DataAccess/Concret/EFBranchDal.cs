using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFBranchDal : EFEntityRepositoryBase<Branch,AppDbContext>, IBranchDal
    {
    }
}
