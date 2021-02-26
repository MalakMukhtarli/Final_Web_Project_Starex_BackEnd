using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Address
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
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
    }
}
