namespace Domain.Entities
{
    public class Project
    {
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public List<Task> Tasks { get;  set; } = new();
        public Guid UserId { get;  set; }

        public Guid ManagerId { get;  set; }

        public Project(string name, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UserId = userId;
        }

        public bool CanBeRemoved()
        {
            return Tasks.All(task => task.Status == ValueObjects.TaskStatus.Completada);
        }
    }
}
