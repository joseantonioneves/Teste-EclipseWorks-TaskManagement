using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace Tests.Application
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly IProjectService _projectService;

        public ProjectServiceTests()
        {
            _projectRepositoryMock = new Mock<IProjectRepository>();
            _projectService = new ProjectService(_projectRepositoryMock.Object);
        }

        [Fact]
        public void CreateProject_ReturnsProjectDto()
        {
            // Arrange
            var projectName = "Test Project";
            var userId = Guid.NewGuid();
            var project = new Project(projectName, userId);

            // Simulando que o método Add será chamado, mas sem retorno
            _projectRepositoryMock.Setup(repo => repo.Add(It.IsAny<Project>()))
                                  .Callback((Project p) => { 
                                      /* Lógica opcional aqui */ 
                                      p.Name = "Nome do Projeto foi Modificado";
                                  });

            // Act
            var result = _projectService.CreateProject(projectName, userId);

            // Assert
            Assert.Equal(projectName, result.Name);
            Assert.Equal(userId, result.UserId);
        }
    }
}
