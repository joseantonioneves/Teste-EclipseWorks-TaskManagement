using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.VisualBasic;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskDto> GetTasksByProjectId(Guid projectId)
        {
            return _repository.GetTasksByProjectId(projectId)
                              .Select(t => new TaskDto
                              {
                                  Id = t.Id,
                                  Title = t.Title,
                                  Description = t.Description,
                                  DueDate = t.DueDate,
                                  Status = t.Status,
                                  Priority = t.Priority,
                                  ProjectId = t.ProjectId,
                                  Comments = t.Comments
                              });
        }

        //public TaskDto CreateTask(Guid projectId, string title, string description, DateTime dueDate, TaskPriority priority, Guid userId)
        //{
        //    var tasks = _repository.GetTasksByProjectId(projectId);
        //    if (tasks.Count() >= 20)
        //        throw new Exception("Cannot add more than 20 tasks to a project.");

        //    var task = new Domain.Entities.Task(title, description, dueDate, priority,userId,projectId);
        //    _repository.AddTaskToProject(projectId, task);

        //    return new TaskDto
        //    {
        //        Id = task.Id,
        //        Title = task.Title,
        //        Description = task.Description,
        //        DueDate = task.DueDate,
        //        Status = task.Status,
        //        Priority = task.Priority,
        //        ProjectId= task.ProjectId,
        //        Comments = new List<string>()
        //    };
        //}

        //public TaskDto CreateTask(TaskDto taskDto)
        //{
        //    // Valide que os dados obrigatórios estão presentes
        //    if (string.IsNullOrWhiteSpace(taskDto.Title))
        //        throw new ArgumentException("Title is required.");
        //    if (taskDto.ProjectId == Guid.Empty)
        //        throw new ArgumentException("ProjectId is required.");
        //    if (taskDto.AssignedUserId is not Guid assignedUserId || assignedUserId == Guid.Empty)
        //        throw new ArgumentException("AssignedUserId is required.");

        //    // Use o construtor parametrizado de Task
        //    var task = new Domain.Entities.Task(
        //        taskDto.Title,
        //        taskDto.Description,
        //        taskDto.DueDate,
        //        taskDto.Priority,
        //        taskDto.ProjectId,
        //        taskDto.AssignedUserId.Value
        //    );

        //    // Adicione a nova tarefa ao repositório
        //    _repository.AddTaskToProject(task.ProjectId, task);

        //    // Retorne o DTO atualizado com o ID da nova tarefa
        //    return new TaskDto
        //    {
        //        Id = task.Id,
        //        Title = task.Title,
        //        Description = task.Description,
        //        DueDate = task.DueDate,
        //        Priority = task.Priority,
        //        ProjectId = task.ProjectId,
        //        AssignedUserId = task.AssignedUserId,
        //        Comments = new List<string>()
        //    };
        //}

        public TaskDto CreateTask(TaskDto taskDto)
        {
            // Converte o DTO em entidade
            var task = new Domain.Entities.Task(
                taskDto.Title,
                taskDto.Description,
                taskDto.DueDate,
                taskDto.Priority,
                taskDto.ProjectId,
                taskDto.AssignedUserId
            );

            _repository.AddTaskToProject(task.ProjectId, task);

            taskDto.Id = task.Id;
            return taskDto;
        }


        //public void UpdateTask(Guid taskId, Domain.ValueObjects.TaskStatus status, string? description = null)
        //{
        //    var task = _repository.GetTaskById(taskId);
        //    if (task == null)
        //        throw new Exception("Task not found.");

        //    task.UpdateStatus(status);

        //    // if (!string.IsNullOrWhiteSpace(description))
        //        task.Description = description;

        //    _repository.UpdateTask(task);
        //}

        public void UpdateTask(Guid taskId, Domain.ValueObjects.TaskStatus newStatus, TaskDto taskDto)
        {
            var task = _repository.GetTaskById(taskId);
            if (task == null)
                throw new Exception("Task not found.");

            // Converte o status de TaskDto para TaskStatus (por exemplo, se o status for uma string, você pode convertê-lo)
            var status = (Domain.ValueObjects.TaskStatus)Enum.Parse(typeof(Domain.ValueObjects.TaskStatus), taskDto.Status.ToString());

            task.UpdateStatus(status);

            if (!string.IsNullOrWhiteSpace(taskDto.Description))
                task.Description = taskDto.Description;

            _repository.UpdateTask(task);
        }


        public void DeleteTask(Guid taskId)
        {
            _repository.RemoveTask(taskId);
        }

        public void AddCommentToTask(Guid taskId, string comment)
        {
            var task = _repository.GetTaskById(taskId);
            if (task == null)
                throw new Exception("Task not found.");

            task.AddComment(comment);
            _repository.UpdateTask(task);
        }
    }
}
