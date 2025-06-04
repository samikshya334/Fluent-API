

using testAPI.Dto;

namespace testAPI.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<List<TaskItemDto>> GetAllTasksAsync();
        Task<List<TaskItemDto>> GetTasksByUserAsync(int userId);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task<List<TaskItem>> GetTasksByStatusAsync(TaskStatus status);
    }

}
