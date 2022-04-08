using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly VacationManagerDbContext _context;

        public TeamRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Team> GetTeams() => this._context.Teams.Include(x => x.TeamLeader).AsQueryable();

        public Team GetTeam(string id) => this._context.Teams.Find(id);

        public void AddTeam(Team team)
        {
            this._context.Teams.Add(team);
            this._context.SaveChanges();
        }

        public void EditTeam(Team team)
        {
            this._context.Teams.Update(team);
            this._context.SaveChanges();
        }

        public void DeleteTeam(Team team)
        {
            this._context.Teams.Remove(team);
            this._context.SaveChanges();
        }
    }
}
