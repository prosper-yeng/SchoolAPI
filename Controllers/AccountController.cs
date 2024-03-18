using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Services;
using SchoolAPI.Model;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
       

        [HttpGet]
        public ActionResult<AuthenticationResponse> Get()
        {
            string token = JWTHelper.GenerateToken("admin@mail.com");
            IEnumerable<AuthenticationResponse> resp = new List<AuthenticationResponse>(){
                new AuthenticationResponse(){
                    Token = token
                }
            };
            return Ok(resp);
        }
    }
}