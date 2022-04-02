using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services 
{
    public class RoleService : IRoleService
    {
        public readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public List<Role> GetRoles() => this._roleRepository.GetRoles().ToList();

        public Role GetRole(string id) => this._roleRepository.GetRole(id);

        public void AddRole(Role role) => this._roleRepository.AddRole(role);

        public void EditRole(Role role) => this._roleRepository.EditRole(role);

        public void DeleteRole(Role role) => this._roleRepository.DeleteRole(role);
    }
}
