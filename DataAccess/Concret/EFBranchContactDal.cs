using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFBranchContactDal:EFEntityRepositoryBase<BranchContact,AppDbContext>, IBranchContactDal
    {
    }
}
