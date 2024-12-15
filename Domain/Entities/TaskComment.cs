namespace Domain.Entities
{
    public class TaskComment
    {
        public Guid Id { get; private set; }
        public Guid TaskId { get; private set; }
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public TaskComment(Guid taskId, string comment)
        {
            Id = Guid.NewGuid();
            TaskId = taskId;
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            CreatedAt = DateTime.UtcNow;
        }
    }
}
