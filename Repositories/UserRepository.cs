using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VacationManagerDbContext _context;

        public UserRepository(VacationManagerDbContext context)
        {
            this._context = context;
        }

        public IQueryable<User> GetUsers() => this._context.Users;

        public User GetUser(string id) => this._context.Users.Find(id);

        public void AddUser(User user)
        {
            this._context.Add(user);
            this._context.SaveChanges();
        }

        public void EditUser(User user)
        {
            this._context.Update(user);
            this._context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            this._context.Remove(user);
            this._context.SaveChanges();
        }
    }
}
