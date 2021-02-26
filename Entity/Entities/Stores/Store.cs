using Core.Entities;
using Entity.Entities.Countries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities.Stores
{
    public class Store: IEntity
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool IsDeleted { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }

    }
}
