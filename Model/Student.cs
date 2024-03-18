using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SchoolAPI.Model
{
    public class Student
    {
     public int Id { get; set; }
     //[RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Name contains invalid characters.")]
     //[StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    }
}




