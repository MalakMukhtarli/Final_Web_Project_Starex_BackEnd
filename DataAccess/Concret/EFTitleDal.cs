using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Titles;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFTitleDal:EFEntityRepositoryBase<Title,AppDbContext>, ITitleDal
    {
    }
}
