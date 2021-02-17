using Entity.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Abstract
{
    public interface IBranchContactService
    {
        BranchContact GetContactWithId(int id);
        List<BranchContact> GetAllContact();
        void Add(BranchContact contact);
        void Update(BranchContact contact);
        void Delete(int id);
    }
}
