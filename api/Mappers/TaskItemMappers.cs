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
                AssignToUserId = taskItem.AssignToUserId,
                DueDate = taskItem.DueDate,
                IsCompleted = taskItem.IsCompleted,
            };
        }

        public static TaskItem toTaskItemFromCreateDto(this CreateTaskItemDto taskItemDto)
        {
            return new TaskItem
            {
                Description = taskItemDto.Description,
                Piority = taskItemDto.Piority,
                Status = taskItemDto.Status,
                Deadline = taskItemDto.Deadline,
                AssignToUserId = taskItemDto.AssignToUserId,
                DueDate = taskItemDto.DueDate,
                IsCompleted = taskItemDto.IsCompleted,
            };
        }

        public static TaskItem toTaskItemFromUpdateDto(this UpdateTaskItemDto taskItemDto)
        {
            return new TaskItem
            {
                Description = taskItemDto.Description,
                Piority = taskItemDto.Piority,
                Status = taskItemDto.Status,
                Deadline = taskItemDto.Deadline,
                AssignToUserId = taskItemDto.AssignToUserId,
                DueDate = taskItemDto.DueDate,
                IsCompleted = taskItemDto.IsCompleted,
            };
        }
    }
}