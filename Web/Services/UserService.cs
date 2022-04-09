using Models;
using Repositories.Interfaces;
using Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Services 
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public List<User> GetUsers() => this._userRepository.GetUsers().ToList();

        public User GetUser(string username) => this._userRepository.GetUser(username);

        public User GetUserById(string id) => this._userRepository.GetUserById(id);

        public void AddUser(User user) => this._userRepository.AddUser(user);

        public void ChangeRole(User user, Role role)
        {
            user.Role = role;
            this._userRepository.EditUser(user);
        }

        public void JoinTeam(User user, Team team)
        {
            user.Team = team;
            this._userRepository.EditUser(user);
        }

        public void LeaveTeam(User user)
        {
            user.Team = null;
            this._userRepository.EditUser(user);
        }

        public void EditUser(User user) => this._userRepository.EditUser(user);

        public void DeleteUser(User user) => this._userRepository.DeleteUser(user);
	}
}