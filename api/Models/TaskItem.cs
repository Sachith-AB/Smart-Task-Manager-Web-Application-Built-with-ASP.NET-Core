using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column("AssignToUserId")]
        [ForeignKey("AssignToUser")]
        public string AssignToUserId { get; set; }

        public AppUser AssignToUser { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}