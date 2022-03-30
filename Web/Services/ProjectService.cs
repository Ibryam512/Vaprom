using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services 
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectService(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public List<Project> GetProjects() => this._projectRepository.GetProjects().ToList();

        public Project GetProject(string id) => this._projectRepository.GetProject(id);

        public void AddProject(Project project) => this._projectRepository.AddProject(project);

        public void AddTeamToProject(Project project, Team team)
        {
            project.Teams.Add(team);
            this._projectRepository.EditProject(project);
        }

        public void RemoveTeamFromProject(Project project, Team team)
        {
            project.Teams.Remove(team);
            this._projectRepository.EditProject(project);
        }

        public void EditProject(Project project) => this._projectRepository.EditProject(project);

        public void DeleteProject(Project project) => this._projectRepository.DeleteProject(project);
    }
}