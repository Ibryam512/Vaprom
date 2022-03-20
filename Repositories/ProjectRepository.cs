using DataAccess;
using Models;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly VacationManagerDbContext _context;

        public ProjectRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Project> GetProjects() => this._context.Projects;

        public Project GetProject(string id) => this._context.Projects.Find(id);

        public void AddProject(Project project)
        {
            this._context.Projects.Add(project);
            this._context.SaveChanges();
        }

        public void EditProject(Project project)
        {
            this._context.Projects.Update(project);
            this._context.SaveChanges();
        }

        public void DeleteProject(Project project)
        {
            this._context.Projects.Remove(project);
            this._context.SaveChanges();
        }
    }
}
