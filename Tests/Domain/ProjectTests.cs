using Domain.Entities;
using Xunit;

namespace Tests.Domain
{
    public class ProjectTests
    {
        [Fact]
        public void Project_CreatesSuccessfully()
        {
            // Arrange
            var projectName = "Test Project";
            var userId = Guid.NewGuid();

            // Act
            var project = new Project(projectName, userId);

            // Assert
            Assert.Equal(projectName, project.Name);
            Assert.Equal(userId, project.UserId);
        }
    }
}
