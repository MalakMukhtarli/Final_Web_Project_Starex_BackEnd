using Core.Entities;
using Entity.Entities.Countries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Addresses
{
    public class Address: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string AddressFirst { get; set; }
        public string AddressSecond { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
