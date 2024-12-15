using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
using System.Xml;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private const string FilePath = "Infrastructure/Data/projects.json";
        private readonly List<Project> _projects;

        public ProjectRepository()
        {
                _projects = new List<Project>();
        }

        public IEnumerable<Project> GetProjectsByUser(Guid userId)
        {
            var projects = LoadProjects();
            return projects.Where(p => p.UserId == userId);
        }

        public IEnumerable<Project> GetProjectsByManagerId(Guid managerId)
        {
            return _projects.Where(p => p.ManagerId == managerId).ToList();
        }

        public Project GetById(Guid projectId)
        {
            var projects = LoadProjects();
            return projects.FirstOrDefault(p => p.Id == projectId);
        }

        public void Add(Project project)
        {
            var projects = LoadProjects().ToList();
            projects.Add(project);
            SaveProjects(projects);
        }

        public void Remove(Project project)
        {
            var projects = LoadProjects().ToList();
            projects.RemoveAll(p => p.Id == project.Id);
            SaveProjects(projects);
        }

        private IEnumerable<Project> LoadProjects()
        {
            if (!File.Exists(FilePath))
                return new List<Project>();

            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Project>>(json);
        }

        private void SaveProjects(IEnumerable<Project> projects)
        {
            var json = JsonConvert.SerializeObject(projects, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}

