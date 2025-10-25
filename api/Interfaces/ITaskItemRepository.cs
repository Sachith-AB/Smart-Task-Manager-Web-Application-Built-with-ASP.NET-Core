using api.Models;

namespace api.Repositories
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetById(int Id);

        Task<TaskItem> CreateTaskItem(TaskItem taskItem);

        Task<TaskItem?> UpdateTaskItem(int Id, TaskItem taskItem);

        Task<bool> DeleteTaskItem(int id);
    }
}