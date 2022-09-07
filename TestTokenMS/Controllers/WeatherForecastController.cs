using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTokenMS.Repository;

namespace TestTokenMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService _userService;
        //******************************************************Constructor DI for Custom service: IAuthService
        public WeatherForecastController(ILogger<WeatherForecastController> logger,  IAuthService userService)
        {
             this._userService = userService;
            _logger = logger;
        }




        //***************************************************8
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            

            var jwtToken = _userService.GenerateToken("nikhil", "1234");
            // return new  { "FREE--Get ALLL-->", jwtToken.auth_token  + "<-ROLE->"+ jwtToken.user_roleId};
            if (jwtToken == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(jwtToken);
        }






        //[AllowAnonymous]
        [Route("getSum")]
        [HttpGet("{id}", Name = "GetSum")]
        public int GetSum(int v1=12,int v2=24)

        {
            return v1 + v2;
        }

            //****************************************************


        }
}
