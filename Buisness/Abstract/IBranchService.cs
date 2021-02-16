using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface IBranchService
    {
        Branch GetBranchWithId(int id);
        List<Branch> GetAllBranch();
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(int id);
    }
}
