using Models;
using System.Collections.Generic;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(string username);
        User GetUserById(string id);
        void AddUser(User user);
        void ChangeRole(User user, Role role);
        void JoinTeam(User user, Team team);
        void LeaveTeam(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}