using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<ProjectDto> GetAllProjects(Guid userId);
        ProjectDto GetProjectById(Guid projectId);
        ProjectDto CreateProject(string projectName, Guid userId);
        void DeleteProject(Guid projectId);
    }
}
