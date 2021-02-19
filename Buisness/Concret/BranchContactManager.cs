using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concret
{
    public class BranchContactManager : IBranchContactService
    {
        private readonly IBranchContactDal _branchContactDal;
        public BranchContactManager(IBranchContactDal branchContactDal)
        {
            _branchContactDal = branchContactDal;
        }

        public async Task<List<BranchContact>> GetAll()
        {
            return await _branchContactDal.GetAll(c =>!c.IsDeleted);
        }

        public async Task<BranchContact> GetWithId(int id)
        {
            return await _branchContactDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        async Task IBranchContactService.Add(BranchContact contact)
        {
            await _branchContactDal.Add(contact);
        }

        async Task IBranchContactService.Delete(int id)
        {
            await _branchContactDal.Delete(new BranchContact { Id = id });
        }

        async Task IBranchContactService.Update(BranchContact contact)
        {
            await _branchContactDal.Update(contact);
        }
    }
}
