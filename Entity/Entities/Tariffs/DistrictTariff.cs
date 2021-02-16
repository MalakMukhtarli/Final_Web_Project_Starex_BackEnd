using Core.Entities;
using Entity.Entities.Branches;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Tariffs
{
    public class DistrictTariff : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
