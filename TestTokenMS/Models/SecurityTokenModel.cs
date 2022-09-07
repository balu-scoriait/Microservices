using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTokenMS.Models
{
    public class SecurityTokenModel
    {
        public string auth_token { get; set; }

        public int user_roleId { get; set; }
    }
}
