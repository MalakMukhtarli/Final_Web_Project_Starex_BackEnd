using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IBranchService
    {
        Task<Branch> GetWithId(int id);
        Task<List<Branch>> GetAll();
        Task Add(Branch branch);
        Task Update(Branch branch);
        Task Delete(int id);
    }
}
