using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProjectDto> GetAllProjects(Guid userId)
        {
            return _repository.GetProjectsByUser(userId)
                              .Select(p => new ProjectDto
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Tasks = p.Tasks.Select(t => new TaskDto
                                  {
                                      Id = t.Id,
                                      Title = t.Title,
                                      Description = t.Description,
                                      DueDate = t.DueDate,
                                      Status = t.Status,
                                      Priority = t.Priority,
                                      Comments = t.Comments
                                  }).ToList()
                              });
        }

        public ProjectDto GetProjectById(Guid projectId)
        {
            var project = _repository.GetById(projectId);
            if (project == null)
                throw new Exception("Project not found.");

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Tasks = project.Tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status,
                    Priority = t.Priority,
                    Comments = t.Comments
                }).ToList()
            };
        }

        public ProjectDto CreateProject(string projectName, Guid userId)
        {
            var project = new Project(projectName, userId);
            _repository.Add(project);

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Tasks = new List<TaskDto>()
            };
        }

        public void DeleteProject(Guid projectId)
        {
            var project = _repository.GetById(projectId);
            if (project == null)
                throw new Exception("Project not found.");

            if (!project.CanBeRemoved())
                throw new Exception("Cannot delete project with pending tasks.");

            _repository.Remove(project);
        }
    }
}
