using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Bios;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concret
{
    public class EFBioDal: EFEntityRepositoryBase<Bio, AppDbContext>, IBioDal
    {
    }
}
