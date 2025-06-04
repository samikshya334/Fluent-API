using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using testAPI.Dto;
using testAPI.Service.Interface;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _taskService.GetTasksByUser(userId);
            return Ok(tasks);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            if (!Enum.TryParse<TaskStatus>(status, true, out var taskStatus))
            {
                return BadRequest("Invalid status value.");
            }

            var tasks = await _taskService.GetTasksByStatus(taskStatus);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest("Task cannot be null.");
            }
            try
            {
               
                var createdTask = await _taskService.CreateTask(taskDto);
                return CreatedAtAction(nameof(GetAllTasks), new { id = createdTask.Id }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                return BadRequest($"Invalid UserId: The specified user does not exist.");
            }
        }

        [HttpPut("{taskId}/status/{status}")]
        public async Task<IActionResult> UpdateStatus(int taskId, string status)
        {
            if (!Enum.TryParse<TaskStatus>(status, true, out var newStatus))
            {
                return BadRequest("Invalid status value.");
            }

            var updated = await _taskService.UpdateStatus(taskId, newStatus);
            if (!updated)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }

            return NoContent();
        }

        private TaskItem MapToModel(TaskItemDto dto)
        {
            return new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                UserId = dto.UserId
            };
        }
    }
}