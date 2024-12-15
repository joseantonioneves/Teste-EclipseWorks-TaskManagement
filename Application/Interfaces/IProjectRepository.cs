using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetProjectsByUser(Guid userId);
        Project GetById(Guid projectId);
        void Add(Project project);
        void Remove(Project project);
        IEnumerable<Project> GetProjectsByManagerId(Guid managerId);
    }
}
