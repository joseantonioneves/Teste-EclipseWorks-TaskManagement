using Task = Domain.Entities.Task;

namespace Application.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<Task> GetTasksByProjectId(Guid projectId);
        Task GetTaskById(Guid taskId);
        void AddTaskToProject(Guid projectId, Task task);
        void UpdateTask(Task task);
        void RemoveTask(Guid taskId);
        public IEnumerable<Task> GetTasksByManagerId(Guid managerId);
    }
}
