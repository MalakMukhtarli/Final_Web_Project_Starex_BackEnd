using Buisness.Abstract;
using DataAccess.Abstract;
using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concret
{
    public class BranchContactManager : IBranchContactService
    {
        private readonly IBranchContactDal _branchContactDal;
        public BranchContactManager(IBranchContactDal branchContactDal)
        {
            _branchContactDal = branchContactDal;
        }
        public void Add(BranchContact contact)
        {
            _branchContactDal.Add(contact);
        }

        public void Delete(int id)
        {
            _branchContactDal.Delete(new BranchContact { Id = id });
        }

        public List<BranchContact> GetAllContact()
        {
            return _branchContactDal.GetAll();
        }

        public BranchContact GetContactWithId(int id)
        {
            return _branchContactDal.Get(c => c.Id == id && !c.IsDeleted);
        }

        public void Update(BranchContact contact)
        {
            _branchContactDal.Update(contact);
        }
    }
}
