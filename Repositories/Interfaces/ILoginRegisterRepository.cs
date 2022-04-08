using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repositories.Interfaces
{
    public interface ILoginRegisterRepository
    {
        void Login(string username, string password);
        void Register(User user);
        void ChangePassword(string username, string newPassword);

        
    }
}
