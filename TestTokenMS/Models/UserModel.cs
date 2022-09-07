using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTokenMS.Models
{
    public class UserModel
    {

        [Required(ErrorMessage ="Username is Required!")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is Required!")]
        public string pwd { get; set; }


        [Required(ErrorMessage = "RoleId is Required!")]
        public int RoleId { get; set; } 

        [Required(ErrorMessage = "IsActive is Required!")]
        public int IsActive { get; set; }


        
        public string CreatedBy { get; set; }


        public DateTime? CreatedDate { get; set; } = null;

        public DateTime? ModifiedDate { get; set; } =null;

        //// [DefaultValue(typeof(DateTime), DateTime.Now.ToString("yyyy-MM-dd"))]


    }
}
