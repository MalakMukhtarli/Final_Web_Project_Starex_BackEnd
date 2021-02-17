using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities.Bios
{
    public class Bio : IEntity
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile LogoHeader { get; set; }
        public string PhotoHeader { get; set; }
        [NotMapped]
        public IFormFile LogoFooter { get; set; }
        public string WorkTime { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
