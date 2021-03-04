using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFAddressDal: EFEntityRepositoryBase<Address, AppDbContext>, IAddressDal
    {
    }
}
