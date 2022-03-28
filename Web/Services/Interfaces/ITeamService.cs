using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services.Interfaces
{
    public interface ITeamService
    {
        List<Team> GetTeams();
        Team GetTeam(string id);
        void AddTeam(Team team);
        void AddTeamLead(User user, Team team);
        void RemoveTeamLead(User user, Team team);
        void AddUserToTeam(User user, Team team);
        void RemoveUserFromTeam(User user, Team team);
        void AddProjectToTeam(Project project, Team team);
        void RemoveProjectFromTeam(Project project, Team team);
        void DeleteTeam(Team team);
    }
}