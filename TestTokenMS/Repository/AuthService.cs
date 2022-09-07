using TestTokenMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using TestTokenMS.helpers;
using Microsoft.Extensions.Configuration;

namespace TestTokenMS.Repository
{
    public class AuthService: IAuthService
    {

        ////
        
            private List<LoginModel> _users = new List<LoginModel>
        {
            new LoginModel {  username = "test", pwd = "test@123" },
            new LoginModel {  username = "gwoodhouse", pwd = "pass@123" },
            new LoginModel {  username = "jsmith", pwd = "admin@123" },
        };

           private readonly helperAppSettings _appSettings;
        private readonly IConfiguration _configuration;
        public AuthService(IOptions<helperAppSettings> appSettings, IConfiguration configuration)
            {
                _appSettings = appSettings.Value;
            _configuration = configuration;
            }

            public SecurityTokenModel GenerateToken(string username, string password,int roleId= 0)
            {
               
            // Generate jwt token for Authenticated Request
            var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var sk = new SymmetricSecurityKey(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    
                    
                    Issuer = "http://ScoriaIT/mainsubsys",
                     Audience = "http://ScoriaIT/subsys",
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, $"{username}"),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(),
                    ClaimValueTypes.Integer64),
                    new Claim("accountnumber", username),
                    new Claim("currency", "$"),
                    new Claim("name", username),
                    new Claim(ClaimTypes.NameIdentifier, username)
                    }),
                    Expires = DateTime.Now.AddMinutes(2),
                   
                    SigningCredentials = new SigningCredentials(sk, SecurityAlgorithms.HmacSha256Signature)
                };
         

           

            var  token = tokenHandler.CreateToken(tokenDescriptor);

            
                var jwtSecurityToken = tokenHandler.WriteToken(token);

            
            //var jwtSecurityToken =new JwtSecurityTokenHandler().WriteToken(token)            // jwtSecurityToken. token.ValidTo;

            return new Models.SecurityTokenModel() { auth_token = jwtSecurityToken,user_roleId=roleId };
            }
        

        ///

    }
}
