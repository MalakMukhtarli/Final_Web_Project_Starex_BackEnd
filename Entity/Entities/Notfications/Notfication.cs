using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities.Notfications
{
    public class Notfication: IEntity
    {
        public int Id { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        public bool IsStored { get; set; }
        public bool IsDeliver { get; set; }
        public bool IsStartDeliver { get; set; }
        public bool IsDeleted { get; set; }
    }
}
