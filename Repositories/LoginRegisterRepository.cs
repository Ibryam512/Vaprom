using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataAccess;
using Repositories.Interfaces;
using Repositories.Helpers;

namespace Repositories
{
   public class LoginRegisterRepository : ILoginRegisterRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly VacationManagerDbContext _context;
        public LoginRegisterRepository(VacationManagerDbContext context, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void ChangePassword(string username,string newPassword)
        {
            if (_context.Users.Any(x => x.UserName == username))
            {
                _context.Users.FirstOrDefault(x => x.UserName == username).PasswordHash = Hasher.Hash(newPassword);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Invalid username");
            }
        }
        
        public void Login(string username, string password)
        {

               if(_context.Users.Any(x=>x.UserName==username))
                {
                    if(_context.Users.FirstOrDefault(x => x.UserName == username).PasswordHash==Hasher.Hash(password))
                    {
                        Logged.User=_context.Users.FirstOrDefault(x => x.UserName == username);
                    }
                    else
                    {
                        throw new ArgumentException("Incorrect username or password");
                    }
                }
                else
                {
                    throw new ArgumentException("Incorrect username or password");
                }

        }

        public void Register(User user)
        {
            if(_context.Users.Any(x=>x.UserName==user.UserName))
            {
                throw new ArgumentException("Потребител със същото име вече съществува");
            }
            _userRepository.AddUser(user);
        }
    }
}
