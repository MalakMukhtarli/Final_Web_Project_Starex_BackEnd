using Core.Entities;
using Entity.Entities.Branches;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities
{
    public class AppUser : IdentityUser, IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Gender { get; set; }
        //[Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PassportId { get; set; }
        [Required]
        public string FinCode { get; set; }
        public bool IsDeleted { get; set; }
        //[ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
