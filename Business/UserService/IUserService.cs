using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.UserService
{
    public interface IUserService
    {
        User GetUserByPhoneNumber(string phoneNumber);
        void RegisterUser(string phoneNumber);
    }
}
