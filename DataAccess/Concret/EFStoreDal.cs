using Core.Repository.EFRepository;
using DataAccess.Abstract;
using Entity.Entities.Stores;

namespace DataAccess.Concret
{
    public class EFStoreDal: EFEntityRepositoryBase<Store, AppDbContext>, IStoreDal
    {
    }
}
