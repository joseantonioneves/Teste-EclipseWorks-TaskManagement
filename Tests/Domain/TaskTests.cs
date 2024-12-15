using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Task = Domain.Entities.Task;
using TaskStatus = Domain.ValueObjects.TaskStatus;

namespace TaskManagement.Tests.Application
{
    public class TaskTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public void CreateTask_ShouldCreateTask_WhenValidDtoIsProvided()
        {
            // Arrange
            var taskDto = new TaskDto
            {
                Title = "Test Task",
                Description = "Test Task Description",
                DueDate = DateTime.Now.AddDays(5),
                Priority = TaskPriority.Média,
                ProjectId = Guid.NewGuid(),
                AssignedUserId = Guid.NewGuid()
            };

            var task = new Task(
                taskDto.Title,
                taskDto.Description,
                taskDto.DueDate,
                taskDto.Priority,
                taskDto.ProjectId,
                taskDto.AssignedUserId
            );

            _taskRepositoryMock.Setup(repo => repo.AddTaskToProject(It.IsAny<Guid>(), It.IsAny<Task>())).Verifiable();
            _taskRepositoryMock.Setup(repo => repo.GetTasksByProjectId(It.IsAny<Guid>())).Returns(new List<Task>());

            // Act
            var result = _taskService.CreateTask(taskDto);

            // Assert
            Assert.Equal(taskDto.Title, result.Title);
            Assert.Equal(taskDto.Description, result.Description);
            Assert.Equal(taskDto.DueDate, result.DueDate);
            Assert.Equal(taskDto.Priority, result.Priority);
            Assert.Equal(taskDto.ProjectId, result.ProjectId);
            Assert.Equal(taskDto.AssignedUserId, result.AssignedUserId);

            _taskRepositoryMock.Verify(repo => repo.AddTaskToProject(It.IsAny<Guid>(), It.IsAny<Task>()), Times.Once);
        }

        [Fact]
        public void CreateTask_ShouldThrowException_WhenTooManyTasksInProject()
        {
            // Arrange
            var taskDto = new TaskDto
            {
                Title = "Test Task",
                Description = "Test Task Description",
                DueDate = DateTime.Now.AddDays(5),
                Priority = TaskPriority.Média,
                ProjectId = Guid.NewGuid(),
                AssignedUserId = Guid.NewGuid()
            };

            _taskRepositoryMock.Setup(repo => repo.GetTasksByProjectId(It.IsAny<Guid>())).Returns(Enumerable.Range(1, 21).Select(x => new Task(
                "Existing Task",
                "Description",
                DateTime.Now,
                TaskPriority.Baixa,
                taskDto.ProjectId,
                Guid.NewGuid()
            )).ToList());

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _taskService.CreateTask(taskDto));
            Assert.Equal("Cannot add more than 20 tasks to a project.", exception.Message);
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTaskStatus_WhenValidTaskIdAndStatusAreProvided()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var taskDto = new TaskDto
            {
                Title = "Test Task",
                Description = "Test Task Description",
                DueDate = DateTime.Now.AddDays(5),
                Priority = TaskPriority.Média,
                ProjectId = Guid.NewGuid(),
                AssignedUserId = Guid.NewGuid()
            };

            var task = new Task(
                taskDto.Title,
                taskDto.Description,
                taskDto.DueDate,
                taskDto.Priority,
                taskDto.ProjectId,
                taskDto.AssignedUserId
            );

            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns(task);
            _taskRepositoryMock.Setup(repo => repo.UpdateTask(It.IsAny<Task>())).Verifiable();

            var newStatus = TaskStatus.EmAndamento;

            // Act
            _taskService.UpdateTask(taskId, newStatus, taskDto);

            // Assert
            Assert.Equal(newStatus, task.Status);
            _taskRepositoryMock.Verify(repo => repo.UpdateTask(It.IsAny<Task>()), Times.Once);
        }

        [Fact]
        public void UpdateTask_ShouldThrowException_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns((Task)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _taskService.UpdateTask(taskId, TaskStatus.Completada, new TaskDto()));
            Assert.Equal("Task not found.", exception.Message);
        }

        [Fact]
        public void DeleteTask_ShouldDeleteTask_WhenValidTaskIdIsProvided()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new Task(
                "Test Task",
                "Test Task Description",
                DateTime.Now.AddDays(5),
                TaskPriority.Média,
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns(task);
            _taskRepositoryMock.Setup(repo => repo.RemoveTask(taskId)).Verifiable();

            // Act
            _taskService.DeleteTask(taskId);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.RemoveTask(taskId), Times.Once);
        }

        [Fact]
        public void AddCommentToTask_ShouldAddComment_WhenValidTaskIdAndCommentAreProvided()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var comment = "This is a comment";
            var task = new Task(
                "Test Task",
                "Test Task Description",
                DateTime.Now.AddDays(5),
                TaskPriority.Média,
                Guid.NewGuid(),
                Guid.NewGuid()
            );

            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns(task);
            _taskRepositoryMock.Setup(repo => repo.UpdateTask(It.IsAny<Task>())).Verifiable();

            // Act
            _taskService.AddCommentToTask(taskId, comment);

            // Assert
            Assert.Contains(comment, task.Comments);
            _taskRepositoryMock.Verify(repo => repo.UpdateTask(It.IsAny<Task>()), Times.Once);
        }

        [Fact]
        public void AddCommentToTask_ShouldThrowException_WhenTaskNotFound()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var comment = "This is a comment";
            _taskRepositoryMock.Setup(repo => repo.GetTaskById(taskId)).Returns((Task)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _taskService.AddCommentToTask(taskId, comment));
            Assert.Equal("Task not found.", exception.Message);
        }
    }
}
