using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IBranchContactService
    {
        Task<BranchContact> GetWithId(int id);
        Task<List<BranchContact>> GetAll();
        Task Add(BranchContact contact);
        Task Update(BranchContact contact);
        Task Delete(int id);
    }
}
