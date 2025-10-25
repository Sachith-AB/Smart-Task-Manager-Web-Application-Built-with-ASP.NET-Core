using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.TaskItems
{
    public class UpdateTaskItemDto
    {
        public string Description { get; set; }

        public string Piority { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public string AssignToUserId { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}