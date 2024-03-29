using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Model
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}