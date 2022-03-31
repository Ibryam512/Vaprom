using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services 
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        
        public TeamService(ITeamRepository teamRepository)
        {
            this._teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
        }

        public List<Team> GetTeams() => this._teamRepository.GetTeams().ToList();

        public Team GetTeam(string id) => this._teamRepository.GetTeam(id);

        public void AddTeam(Team team) => this._teamRepository.AddTeam(team);

        public void AddTeamLead(User user, Team team)
        {
            team.TeamLeader = user;
            this._teamRepository.EditTeam(team);
        }

        public void RemoveTeamLead(Team team)
        {
            team.TeamLeader = null;
            this._teamRepository.EditTeam(team);
        }

        public void AddUserToTeam(User user, Team team)
        {
            team.Developers.Add(user);
            this._teamRepository.EditTeam(team);
        }

        public void RemoveUserFromTeam(User user, Team team)
        {
            team.Developers.Remove(user);
            this._teamRepository.EditTeam(team);
        }

        public void AddProjectToTeam(Project project, Team team)
        {
            team.Project = project;
            this._teamRepository.EditTeam(team);
        }

        public void RemoveProjectFromTeam(Team team)
        {
            team.Project = null;
            this._teamRepository.EditTeam(team);
        }

        public void EditTeam(Team team) => this._teamRepository.EditTeam(team);

        public void DeleteTeam(Team team) => this._teamRepository.DeleteTeam(team);
    }
}