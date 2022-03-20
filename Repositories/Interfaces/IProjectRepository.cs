using Models;
using System.Linq;

namespace Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetProjects();
        Project GetProject(string id);
        void AddProject(Project project);
        void EditProject(Project project);
        void DeleteProject(Project project);
    }
}
