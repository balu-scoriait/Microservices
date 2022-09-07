using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTokenMS.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Username is Required!")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is Required!")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "App-Code is Required!")]
        public string appcode { get; set; } 

        [Required(ErrorMessage = "RoleId is Required!")]
        public int RoleId { get; set; } 


        [Required(ErrorMessage = "IsActive is Required!")]
        public bool IsActive { get; set; } 

        
        public string CreatedBy { get; set; } 
    }
}