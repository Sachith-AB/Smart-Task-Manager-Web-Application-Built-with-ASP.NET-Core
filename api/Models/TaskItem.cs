using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public string Piority { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public string AssignedToUserId { get; set; }
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}