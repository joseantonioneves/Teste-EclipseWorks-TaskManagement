using Api.Controllers;
using Application.DTOs;
using Application.Interfaces;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TaskStatus = Domain.ValueObjects.TaskStatus;

namespace Tests.Api
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskService> _taskServiceMock;
        private readonly TaskController _controller;

        public TasksControllerTests()
        {
            _taskServiceMock = new Mock<ITaskService>();
            _controller = new TaskController(_taskServiceMock.Object);
        }

        [Fact]
        public void GetTasksByProject_ReturnsTasks()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var tasks = new List<TaskDto>
            {
                new TaskDto { Id = Guid.NewGuid(), Title = "Task 1", ProjectId = projectId },
                new TaskDto { Id = Guid.NewGuid(), Title = "Task 2", ProjectId = projectId }
            };

            _taskServiceMock.Setup(service => service.GetTasksByProjectId(projectId))
                            .Returns(tasks);

            // Act
            var result = _controller.GetTasksByProject(projectId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTasks = Assert.IsAssignableFrom<IEnumerable<TaskDto>>(okResult.Value);
            Assert.Equal(2, returnTasks.Count());
        }

        [Fact]
        public void CreateTask_ReturnsCreatedTask()
        {
            // Arrange
            var taskDto = new TaskDto
            {
                Title = "New Task",
                Description = "Description",
                ProjectId = Guid.NewGuid()
            };
            var createdTask = new TaskDto { Id = Guid.NewGuid(), Title = taskDto.Title };

            _taskServiceMock.Setup(service => service.CreateTask(taskDto))
                            .Returns(createdTask);

            // Act
            var result = _controller.CreateTask(taskDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnTask = Assert.IsType<TaskDto>(createdResult.Value);
            Assert.Equal(taskDto.Title, returnTask.Title);
        }

        [Fact]
        public void UpdateTask_ReturnsNoContent()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var taskDto = new TaskDto { Title = "Updated Task" };
            var taskStatus = TaskStatus.Pendente;

            _taskServiceMock.Setup(service => service.UpdateTask(taskId, taskStatus, taskDto));

            // Act
            var result = _controller.UpdateTask(taskId, taskStatus, taskDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteTask_ReturnsNoContent()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _taskServiceMock.Setup(service => service.DeleteTask(taskId));

            // Act
            var result = _controller.DeleteTask(taskId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
