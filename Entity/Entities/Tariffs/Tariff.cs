using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entity.Entities.Countries;

namespace Entity.Entities.Tariffs
{
    public class Tariff : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Weight { get; set; } 
        [Required]
        public double StartWeight { get; set; }
        [Required]
        public double EndWeight { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsLiquid { get; set; }
        public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
