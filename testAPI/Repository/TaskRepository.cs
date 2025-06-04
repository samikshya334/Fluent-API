using Microsoft.EntityFrameworkCore;
using testAPI.Data;
using testAPI.Models;
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

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems
                                .Include(t => t.User)
                                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetTasksByUserAsync(int userId)
        {
            return await _context.TaskItems
                                .Include(t => t.User)
                                .Where(t => t.UserId == userId)
                                .ToListAsync();
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

        public async Task<List<TaskItem>> GetTasksByStatusAsync(TaskStatus status)
        {
            return await _context.TaskItems
                                .Include(t => t.User)
                                .Where(t => t.Status == status)
                                .ToListAsync();
        }

        public Task<List<TaskItem>> GetTasksByStatusAsync(System.Threading.Tasks.TaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}