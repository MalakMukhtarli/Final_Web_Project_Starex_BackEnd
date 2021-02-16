using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class BranchManager : IBranchService
    {
        private readonly IBranchDal _branchDal;
        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }
        public void Add(Branch branch)
        {
            _branchDal.Add(branch);
        }

        public void Delete(int id)
        {
            _branchDal.Delete(new Branch { Id = id });
        }

        public List<Branch> GetAllBranch()
        {
            return _branchDal.GetAll(b => !b.IsDeleted);
        }

        public Branch GetBranchWithId(int id)
        {
            return _branchDal.Get(b => b.Id == id && !b.IsDeleted);
        }

        public void Update(Branch branch)
        {
            _branchDal.Update(branch);
        }
    }
}
