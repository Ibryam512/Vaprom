using DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Interfaces;
using System;
using System.Linq;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VacationManagerDbContext _context;

        public UserRepository(VacationManagerDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<User> GetUsers() => this._context.Users.Include(x => x.Team).Include(x => x.Role).AsQueryable();

        public User GetUser(string username) => this._context.Users.Include(x => x.Team).Include(x => x.Role).FirstOrDefault(x => x.UserName == username);

        public User GetUserById(string id) => this._context.Users.FirstOrDefault(x => x.Id == id);

        public void AddUser(User user)
        {
            this._context.Users.Add(user);
            this._context.SaveChanges();
        }

        public void EditUser(User user)
        {
            this._context.Users.Update(user);
            this._context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            this._context.Users.Remove(user);
            this._context.SaveChanges();
        }
	}
}
