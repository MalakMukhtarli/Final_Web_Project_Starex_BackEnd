using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Branches;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concret
{
    public class EFBranchDal : EFEntityRepositoryBase<Branch, AppDbContext>, IBranchDal
    {
    }
}
