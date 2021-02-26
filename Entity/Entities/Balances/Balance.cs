using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Balancess
{
    public class Balance:IEntity
    {
        public int Id { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public double? MyBalance{ get; set; }

    }
}
