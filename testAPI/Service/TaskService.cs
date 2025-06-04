using System.Linq;
using testAPI.Dto;
using testAPI.Repository.Interface;
using testAPI.Service.Interface;

namespace testAPI.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTasks()
        {
            var tasks = await _repo.GetAllTasksAsync();
            return (IEnumerable<TaskItemDto>)tasks;
        }
            
        public async Task<IEnumerable<TaskItemDto>> GetTasksByUser(int userId)
        {
            var tasks = await _repo.GetTasksByUserAsync(userId);
            return (IEnumerable<TaskItemDto>)tasks;
        }

        public async Task<IEnumerable<TaskItemDto>> GetTasksByStatus(TaskStatus status)
        {
            var tasks = await _repo.GetTasksByStatusAsync(status);
            return (IEnumerable<TaskItemDto>)tasks; 
        }

        public async Task<TaskItemDto> CreateTask(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var userExists = await _repo.GetByIdAsync(task.UserId) != null;
            if (!userExists)
            {
                throw new ArgumentException($"User with ID {task.UserId} does not exist.", nameof(task.UserId));
            }

            await _repo.AddAsync(task);
            return MapToDto(task);
        }

        public async Task<bool> UpdateStatus(int taskId, TaskStatus newStatus)
        {
            var task = await _repo.GetByIdAsync(taskId);
            if (task == null)
            {
                return false;
            }

            task.Status = (Utility.TaskStatus)newStatus;
            task.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(task);
            return true;
        }

        private TaskItemDto MapToDto(TaskItem task)
        {
            return new TaskItemDto
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                UserId = task.UserId
            };
        }
    }
}