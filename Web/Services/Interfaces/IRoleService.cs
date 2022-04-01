using Models;
using System.Collections.Generic;

namespace Web.Services.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role GetRole(string id);
        void AddRole(Role role);
        void EditRole(Role role);
        void DeleteRole(Role role);
    }
}