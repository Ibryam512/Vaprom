using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsers();
        User GetUser(string id);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}
