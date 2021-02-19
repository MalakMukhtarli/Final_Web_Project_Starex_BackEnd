using Core.Entities;
using Entity.Entities.Countries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities.Declarations
{
    public class Declaration : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Shop { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        public string TrackingNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Note { get; set; }
        public bool Condition { get; set; }
        public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
