using Microsoft.EntityFrameworkCore;
using testAPI.Data;
using testAPI.Dto;
using testAPI.Repository.Interface;
using testAPI.Utility;
using TaskStatus = testAPI.Utility.TaskStatus;

namespace testAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<TaskItemDto>> GetAllTasksAsync()
        {
            var tasks = await _context.TaskItems
                .Include(t => t.User)
                .ToListAsync();

            return tasks.Select(t => MapToDto(t)).ToList();
        }

        public async Task<List<TaskItemDto>> GetTasksByUserAsync(int userId)
        {
            var tasks = await _context.TaskItems
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return tasks.Select(t => MapToDto(t)).ToList();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.TaskItems
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskItemDto>> GetTasksByStatusAsync(TaskStatus status)
        {
            var tasks = await _context.TaskItems
                .Include(t => t.User)
                .Where(t => t.Status == status)
                .ToListAsync();

            return tasks.Select(t => MapToDto(t)).ToList();
        }

        private TaskItemDto MapToDto(TaskItem task)
        {
            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                UserId = task.UserId,
                User = task.User != null ? new UserDto
                {
                    Id = task.User.Id,
                    Name = task.User.Name 
                } : null
            };
        }

        public Task<List<TaskItem>> GetTasksByStatusAsync(System.Threading.Tasks.TaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}