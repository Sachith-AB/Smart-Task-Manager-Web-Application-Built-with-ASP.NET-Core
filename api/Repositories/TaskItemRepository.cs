using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class TaskItemRepository(ApplicationDBContext context) : ITaskItemRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<TaskItem> CreateTaskItem(TaskItem taskItem)
        {
            await _context.TaskItem.AddAsync(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItem.FindAsync(id);

            if (taskItem == null)
                return false;

            _context.TaskItem.Remove(taskItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<TaskItem>> GetAllAsync()
        {
            return _context.TaskItem.ToListAsync();
        }

        public async Task<TaskItem?> GetById(int Id)
        {
            return await _context.TaskItem.FindAsync(Id);
        }

        public async Task<TaskItem?> UpdateTaskItem(int Id, TaskItem taskItem)
        {
            var existingItem = await _context.TaskItem.FindAsync(Id);

            if (existingItem == null)
                return null;

            existingItem.Description = taskItem.Description;
            existingItem.Piority = taskItem.Piority;
            existingItem.Status = taskItem.Status;
            existingItem.Deadline = taskItem.Deadline;
            existingItem.AssignToUserId = taskItem.AssignToUserId;
            existingItem.DueDate = taskItem.DueDate;
            existingItem.IsCompleted = taskItem.IsCompleted;

            await _context.SaveChangesAsync();
            return existingItem;
        }
    }
}