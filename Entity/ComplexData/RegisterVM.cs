//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Entity.ComplexData
//{
//    public class RegisterVM
//    {
//        [Required]
//        public string Name { get; set; }
//        [Required]
//        public string Surname { get; set; }
//        [Required]
//        public string UserName { get; set; }
//        [Required, DataType(DataType.EmailAddress)]
//        public string Email { get; set; }
//        [Required, DataType(DataType.Password)]
//        public string Password { get; set; }
//        [Required, DataType(DataType.Password), Compare(nameof(Password))]
//        public string CheckPassword { get; set; }
//        [Required]
//        public string Gender { get; set; }
//        [Required]
//        public DateTime? Birthday { get; set; }
//        [Required]
//        public string Address { get; set; }
//        [Required]
//        public int PassportId { get; set; }
//        [Required]
//        public string FinCode { get; set; }
//    }
//}
