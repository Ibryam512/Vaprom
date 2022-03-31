using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services.Interfaces
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        Project GetProject(string id);
        void AddProject(Project project);
        void AddTeamToProject(Project project, Team team);
        void RemoveTeamFromProject(Project project, Team team);
        void EditProject(Project project);
        void DeleteProject(Project project);
    }
}