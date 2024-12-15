using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers;
using Application.Interfaces;
using Application.DTOs;
using System;

namespace Tests.Api
{
    public class ProjectsControllerTests
    {
        //private readonly ProjectsController _controller;
        private readonly ProjectController _controller;
        private readonly Mock<IProjectService> _projectServiceMock;

        public ProjectsControllerTests()
        {
            // Inicializa o mock e o controller
            _projectServiceMock = new Mock<IProjectService>();
            _controller = new ProjectController(_projectServiceMock.Object);
        }

        [Fact]
        public void GetProjectById_ReturnsOkResult_WithProject()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var projectDto = new ProjectDto
            {
                Id = projectId,
                Name = "Test Project"
            };

            _projectServiceMock
                .Setup(service => service.GetProjectById(projectId))
                .Returns(projectDto);

            // Act
            var result = _controller.GetProjectById(projectId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProjectDto>(okResult.Value);
            Assert.Equal(projectId, returnValue.Id);
            Assert.Equal("Test Project", returnValue.Name);
        }

        [Fact]
        public void CreateProject_ReturnsCreatedAtAction()
        {
            // Arrange
            var projectName = "Novo Projeto";
            var userId = Guid.NewGuid();

            _projectServiceMock.Setup(service => service.CreateProject(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(new ProjectDto { Id = Guid.NewGuid(), Name = "New Project" });

            // Act
            var result = _controller.CreateProject(projectName, userId);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);  // Verifica se o tipo é CreatedAtActionResult
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.NotNull(createdAtActionResult);
            Assert.IsType<ProjectDto>(createdAtActionResult.Value);  // Verifica se o valor retornado é um ProjectDto
            var returnProject = createdAtActionResult.Value as ProjectDto;
            Assert.Equal("New Project", returnProject.Name);  // Verifica se o título do projeto está correto
        }



        [Fact]
        public void DeleteProject_ReturnsNoContent_WhenProjectExists()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            _projectServiceMock
                .Setup(service => service.DeleteProject(projectId))
                .Verifiable();

            // Act
            var result = _controller.DeleteProject(projectId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _projectServiceMock.Verify(service => service.DeleteProject(projectId), Times.Once);
        }
    }
}
