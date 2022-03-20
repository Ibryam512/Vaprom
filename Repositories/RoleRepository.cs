using DataAccess;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VacationManagerDbContext _context;

        public RoleRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Role> GetRoles() => this._context.Roles;

        public Role GetRole(string name) => this._context.Roles.Find(name);

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
