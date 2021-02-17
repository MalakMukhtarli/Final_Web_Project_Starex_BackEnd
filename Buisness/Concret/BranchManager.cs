using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class BranchManager : IBranchService
    {
        private readonly IBranchDal _branchDal;
        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }

        public async Task<List<Branch>> GetAll()
        {
            return await _branchDal.GetAll(b => !b.IsDeleted);
        }

        public async Task<Branch> GetWithId(int id)
        {
            return await _branchDal.Get(b => b.Id == id && !b.IsDeleted);
        }

        async Task IBranchService.Add(Branch branch)
        {
            await _branchDal.Add(branch);
        }

        async Task IBranchService.Delete(int id)
        {
            await _branchDal.Delete(new Branch { Id = id });
        }

        async Task IBranchService.Update(Branch branch)
        {
            await _branchDal.Update(branch);
        }
    }
}
