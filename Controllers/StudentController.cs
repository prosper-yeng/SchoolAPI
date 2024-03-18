using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Model;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context; //protects the constructor from modification
      public StudentController(AppDbContext context)//Getting the database
      {
        _context = context;
      }
       
       //[Authorize]
       [HttpGet]
       [Route("/student")]
       public IActionResult GetStudents() //displays all students
       {
        var students = _context.Students.ToList(); // ToList is for sql default execution
        return Ok(students);//"Reading all the students";
       }

       
      // [Authorize]
       [HttpGet]
       [Route("/student{id}")]
       //IActionResult is a rapper that simplifies code complexities
       public IActionResult GetStudenById([FromRoute] int id) //display a student record by id
       {
        var student = _context.Students.Find(id);
        if(student==null)
        {
            return NotFound();//NotFound is a wrapper to helps save time in writing a bunch of code to indicate result not found
        }

        return Ok(student); // $"Reading student: {id}";
        }


        //[Authorize]
        [HttpPost]
        [Route("/student")]
        public IActionResult CreateStudent(Student student)//Creats a student record
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudenById), new {id = student.Id},student);
        }
        
        //[Authorize]
        //[EnableCors("AllowOrigin")]    
        [HttpPut]
        [Route("/student{id}")]
        public IActionResult UpdateStudent(Student student)//Updates a student record based on the id
        {
            _context.Students.Update(student);
            _context.SaveChanges();
            return Ok(); // $"Updating a student: {id}"
        }

        //[Authorize]
        [HttpDelete]
        [Route("/student{id}")]
        public IActionResult DeleteStudent([FromRoute] int id) //Deletes a specific student's record
        {
            var student = _context.Students.Find(id);
            if (student== null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok(); // $"Deleting a student: {id}";
        }
    }
}