using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public ValueObjects.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<string> Comments { get; set; } = new();
        public Guid UserId { get; set; } // Adicionando o UserId para associar a tarefa a um usuário
        public Guid ProjectId { get; set; } // associação a um projeto
        public Guid? AssignedUserId { get; set; }
        public object Name { get; set; }

        public Task(string title, string description, DateTime dueDate, TaskPriority priority, Guid userId, Guid projectId)
        {
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            DueDate = dueDate;
            Priority = priority;
            Status = ValueObjects.TaskStatus.Pendente;
            AssignedUserId = userId;  // Associando a tarefa a um usuário
            ProjectId = projectId;
        }

        public void UpdateStatus(ValueObjects.TaskStatus status)
        {
            Status = status;
        }

        public void AddComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Comment cannot be empty.");

            Comments.Add(comment);
        }
    }
}
