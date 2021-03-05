using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities.Titles
{
    public class Title : IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
    }
}
