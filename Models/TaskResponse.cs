using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Models
{
    public class TaskResponse
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Please enter a valid task name.")]
        public string TaskName { get; set; }
        public string DueDate { get; set; }
        [Required(ErrorMessage = "Please select a valid quadrant.")]
        public string Quadrant { get; set; }
        public bool Completed { get; set; }

        //build foreign key relationship
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
