using DataAccess.Data;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private readonly LifitDbContext _context;

        public UserRepository(LifitDbContext context)
        {
            _context = context;
        }

        public bool IsExisUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
