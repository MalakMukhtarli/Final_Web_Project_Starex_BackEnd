using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Entities.Newss
{
    public class News : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string ImageBig { get; set; }
        [NotMapped]
        public IFormFile PhotoBig { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        //public virtual NewsDetail NewsDetail { get; set; }
    }
}
