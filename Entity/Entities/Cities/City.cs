using Core.Entities;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Cities
{
    public class City : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
