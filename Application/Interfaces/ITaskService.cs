using Application.DTOs;
using Domain.ValueObjects;
using System.Collections;
using TaskStatus = Domain.ValueObjects.TaskStatus;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetTasksByProjectId(Guid projectId);
        TaskDto CreateTask(TaskDto taskDto); // (Guid projectId, string title, string description, DateTime dueDate, TaskPriority priority,Guid userId);
        void UpdateTask(Guid taskId, Domain.ValueObjects.TaskStatus newStatus, TaskDto taskDto); // Aceita TaskDto em vez de TaskStatus
        //void UpdateTask(Guid taskId, TaskStatus status, string? description = null);
        void DeleteTask(Guid taskId);
        void AddCommentToTask(Guid taskId, string comment);
    }
}
