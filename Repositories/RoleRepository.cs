using DataAccess;
using Models;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VacationManagerDbContext _context;

        public RoleRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Role> GetRoles() => this._context.Roles.AsQueryable();

        public Role GetRole(string id) => this._context.Roles.Find(id);

        public void AddRole(Role role)
        {
            this._context.Roles.Add(role);
            this._context.SaveChanges();
        }

        public void EditRole(Role role)
        {
            this._context.Roles.Update(role);
            this._context.SaveChanges();
        }

        public void DeleteRole(Role role)
        {
            this._context.Roles.Remove(role);
            this._context.SaveChanges();
        }
    }
}
