using TestTokenMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTokenMS.Repository
{
   public  interface IAuthService
    {
        SecurityTokenModel GenerateToken(string username, string password,int roleId=0);
    }
}
