using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Model;
//using Microsoft.AspNetCore.Identity; // Add this to your using directives
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;
using SchoolAPI.Services;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolUserController : ControllerBase
    {
        private readonly AppDbContext _context; //protects the constructor from modification
       // private readonly PasswordHasher<SchoolUser> _passwordHasher;
      public SchoolUserController(AppDbContext context)//Getting the database
      
      {
        _context = context;
      
      }

       [HttpGet]
       [Route("/SchoolUser")]
       public IActionResult GetSchoolUsers() //displays all users
       {
        var schoolUsers = _context.SchoolUsers.ToList(); // ToList is for sql default execution
        return Ok(schoolUsers);//"Reading all the users";
       }

       [HttpGet]
       [Route("/schoolUser{id}")]
       //IActionResult is a rapper that simplifies code complexities
       public IActionResult GetStudenById([FromRoute] long id) //display a schoolUser record by id
       {
        var schoolUser = _context.SchoolUsers.Find(id);
        if(schoolUser==null)
        {
            return NotFound();//NotFound is a wrapper to helps save time in writing a bunch of code to indicate result not found
        }

        return Ok(schoolUser); // $"Reading schoolUser: {id}";
        }

        [HttpPost]
        [Route("/schoolUsers")]
        public IActionResult CreateSchoolUser(SchoolUser schoolUser)//Creats a schoolUser record
        {
            string salt = BC.GenerateSalt();
            schoolUser.PasswordHash=BC.HashPassword(schoolUser.PasswordHash,salt);
            _context.SchoolUsers.Add(schoolUser);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudenById), new {id = schoolUser.Id},schoolUser);
        }
        
        
        [HttpPut]
        [Route("/schoolUser{id}")]
        public IActionResult UpdateSchoolUser(SchoolUser schoolUser)//Updates a schoolUser record based on the id
        {
            _context.SchoolUsers.Update(schoolUser);
            _context.SaveChanges();
            return Ok(); // $"Updating a schoolUser: {id}"
        }

        [HttpDelete]
        [Route("/schoolUser{id}")]
        public IActionResult DeleteSchoolUser([FromRoute] int id) //Deletes a specific schoolUser's record
        {
            var schoolUser = _context.SchoolUsers.Find(id);
            if (schoolUser== null)
            {
                return NotFound();
            }

            _context.SchoolUsers.Remove(schoolUser);
            _context.SaveChanges();
            return Ok(); // $"Deleting a schoolUser: {id}";
        }
    
        [HttpPost]
       [Route("/schoolUsers/login")]
       public IActionResult Login([FromBody] LoginModel loginModel) //
       {
          var myuser = _context.SchoolUsers.Where(x=>x.Email==loginModel.username).FirstOrDefault();
          if (myuser==null)
          {
            return Unauthorized();            
          }
           // Verify the hashed password
            if( BC.Verify( loginModel.password,myuser.PasswordHash))
            {
                string token = JWTHelper.GenerateToken(loginModel.username);
                IEnumerable<AuthenticationResponse> resp = new List<AuthenticationResponse>()
            {
                new AuthenticationResponse()
                {
                    Token = token
                }
            };
            return Ok(resp);
            }
             else
            {
                return BadRequest(new { message = "Invalid email or password." });
            }
             
          
         //return Ok(new { });
        //return Unauthorized();
        
       }
    }

}
