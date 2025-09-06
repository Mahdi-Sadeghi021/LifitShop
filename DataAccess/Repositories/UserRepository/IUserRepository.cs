using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserRepository
    {
        bool IsExisUserByPhoneNumber(string phoneNumber);
        void AddUser(User user);
    }
}
