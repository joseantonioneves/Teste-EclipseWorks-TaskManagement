using Domain.ValueObjects;

namespace Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Domain.ValueObjects.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<string> Comments { get; set; }
        public Guid ProjectId { get; set; }
        public object Name { get; set; }
        public Guid AssignedUserId { get; set; }
    }
}
