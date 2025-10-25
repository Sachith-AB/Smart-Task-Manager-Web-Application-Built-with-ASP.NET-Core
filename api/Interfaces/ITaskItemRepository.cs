using api.Helpers;
using api.Models;

namespace api.Repositories
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllAsync(QueryObject queryObject);

        Task<TaskItem?> GetById(int Id);

        Task<TaskItem> CreateTaskItem(TaskItem taskItem);

        Task<TaskItem?> UpdateTaskItem(int Id, TaskItem taskItem);

        Task<bool> DeleteTaskItem(int id);
    }
}