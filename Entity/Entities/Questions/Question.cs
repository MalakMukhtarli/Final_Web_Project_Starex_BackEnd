using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Entities.Questions
{
    public class Question:IEntity
    {
        public int Id { get; set; }
        public string AskedQuestion { get; set; }
        public string ResponseQuestion { get; set; }
        public QuestionNavbar QuestionNavbar { get; set; }
        public int QuestionNavbarId { get; set; }
        public bool IsDelete { get; set; }



    }
}
