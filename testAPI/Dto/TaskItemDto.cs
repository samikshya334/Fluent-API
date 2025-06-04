using testAPI.Utility;
using TaskStatus = testAPI.Utility.TaskStatus;

namespace testAPI.Dto
{
   
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; } 
    }
}
