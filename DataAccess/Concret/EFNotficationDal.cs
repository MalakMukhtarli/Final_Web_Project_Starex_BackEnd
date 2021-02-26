using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Notfications;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFNotficationDal: EFEntityRepositoryBase<Notfication, AppDbContext>, INotficationDal
    {
    }
}
