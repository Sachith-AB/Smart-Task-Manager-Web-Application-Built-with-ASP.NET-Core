using api.Data;
using api.Helpers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<List<TaskItem>> GetAllAsync(QueryObject queryObject)
        {
            var taskItems = _context.TaskItem.AsQueryable();

            // Apply search filter first (case-insensitive)
            if (!string.IsNullOrWhiteSpace(queryObject.SearchTerm))
            {
                var searchLower = queryObject.SearchTerm.ToLower();
                taskItems = taskItems.Where(x => x.Description.ToLower().Contains(searchLower));
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.ToLower() == "description")
                {
                    taskItems = queryObject.IsDescending
                        ? taskItems.OrderByDescending(x => x.Description)
                        : taskItems.OrderBy(x => x.Description);
                }
                else if (queryObject.SortBy.ToLower() == "deadline")
                {
                    taskItems = queryObject.IsDescending
                        ? taskItems.OrderByDescending(x => x.Deadline)
                        : taskItems.OrderBy(x => x.Deadline);
                }
                else if (queryObject.SortBy.ToLower() == "status")
                {
                    taskItems = queryObject.IsDescending
                        ? taskItems.OrderByDescending(x => x.Status)
                        : taskItems.OrderBy(x => x.Status);
                }
                else if (queryObject.SortBy.ToLower() == "piority")
                {
                    taskItems = queryObject.IsDescending
                        ? taskItems.OrderByDescending(x => x.Piority)
                        : taskItems.OrderBy(x => x.Piority);
                }
            }

            return await taskItems.ToListAsync();
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