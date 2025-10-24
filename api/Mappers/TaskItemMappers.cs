using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.TaskItems;
using api.Models;

namespace api.Mappers
{
    public static class TaskItemMappers
    {
        public static TaskItemDto toTaskItemDto(this TaskItem taskItem)
        {
            return new TaskItemDto
            {
                Description = taskItem.Description,
                Piority = taskItem.Piority,
                Status = taskItem.Status,
                Deadline = taskItem.Deadline,
                AssignedToUserId = taskItem.AssignedToUserId,
                DueDate = taskItem.DueDate,
                IsCompleted = taskItem.IsCompleted,
            };
        }
    }
}