using testAPI.Models;

namespace testAPI.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<List<TaskItem>> GetTasksByUserAsync(int userId);
        Task<List<TaskItem>> GetTasksByStatusAsync(TaskStatus status);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
    }

}
