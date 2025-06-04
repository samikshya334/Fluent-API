using testAPI.Dto;
using testAPI.Models;

namespace testAPI.Service.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllTasks();
        Task<IEnumerable<TaskItemDto>> GetTasksByUser(int userId);
        Task<IEnumerable<TaskItemDto>> GetTasksByStatus(TaskStatus status);
        Task<TaskItemDto> CreateTask(TaskItem task);
        Task<bool> UpdateStatus(int taskId, TaskStatus status);
    }
}