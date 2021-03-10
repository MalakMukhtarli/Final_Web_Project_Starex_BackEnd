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
        public double? Price { get; set; }
        public string Currency { get; set; }
        public double? MyBalance{ get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
