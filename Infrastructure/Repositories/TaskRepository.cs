using Application.Interfaces;
using Domain.Entities;
using Task = Domain.Entities.Task;
using TaskStatus = Domain.ValueObjects.TaskStatus;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<Task> _tasks; // Pode ser substituído por acesso a um banco de dados
        private readonly IProjectRepository _projectRepository;

        public TaskRepository(IProjectRepository projectRepository)
        {
            _tasks = new List<Task>();
            _projectRepository = projectRepository;
            
        }

        public IEnumerable<Task> GetTasksByProjectId(Guid projectId)
        {
            return _tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public Task GetTaskById(Guid taskId)
        {
            return _tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Task> GetTasksByManagerId(Guid managerId)
        {
            // Supondo que _projects contenha a lista de projetos
            var managedProjects = _projectRepository.GetProjectsByManagerId(managerId).Select(p => p.Id);

            // Filtra as tarefas desses projetos
            return _tasks.Where(t => managedProjects.Contains(t.ProjectId)).ToList();
        }



        public void AddTaskToProject(Guid projectId, Task task)
        {
            _tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            var existingTask = GetTaskById(task.Id);
            if (existingTask != null)
            {
                existingTask = task;
            }
        }

        public void RemoveTask(Guid taskId)
        {
            var task = GetTaskById(taskId);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }
        public IEnumerable<Task> GetCompletedTasksByUser(Guid userId, DateTime startDate, DateTime endDate)
        {
            // Filtra as tarefas concluídas do usuário dentro do período especificado
            return _tasks.Where(t => t.Status == TaskStatus.Completada &&
                                      t.DueDate >= startDate &&
                                      t.DueDate <= endDate).ToList();
        }
       
    }
}