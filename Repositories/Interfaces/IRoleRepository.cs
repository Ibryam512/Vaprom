using Models;
using System.Linq;

namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<Role> GetRoles();
        Role GetRole(string name);
        void AddRole(Role role);
        void EditRole(Role role);
        void DeleteRole(Role role);
    }
}
