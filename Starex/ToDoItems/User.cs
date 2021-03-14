using Entity.Entities.Balancess;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starex.ToDoItems
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public int PassportId { get; set; }
        public string FinCode { get; set; }
        public string Role { get; set; }
        public List<string> Roles { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int BalanceId { get; set; }
        public Balance Balance { get; set; }
    }
}
