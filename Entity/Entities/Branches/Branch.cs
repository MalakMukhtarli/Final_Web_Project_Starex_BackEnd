using Core.Entities;
using Entity.Entities.Cities;
using Entity.Entities.Contacts;
using Entity.Entities.Tariffs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Branches
{
    public class Branch : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public virtual ICollection<DistrictTariff> DistrictTariffs { get; set; }
        public virtual ICollection<BranchContact> BranchContacts { get; set; }
    }
}
