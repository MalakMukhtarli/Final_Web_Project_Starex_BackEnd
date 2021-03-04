using Core.Entities;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities.Contacts
{
    public class BranchContact : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Map { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
