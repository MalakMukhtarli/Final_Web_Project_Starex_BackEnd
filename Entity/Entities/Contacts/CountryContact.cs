using Core.Entities;
using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Contacts
{
    public class CountryContact : IEntity 
    { 
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Time { get; set; }
        public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
