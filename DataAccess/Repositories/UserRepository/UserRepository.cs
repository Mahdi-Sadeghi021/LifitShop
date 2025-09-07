using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly LifitDbContext _context;

        public UserRepository(LifitDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// گرفتن کاربر بر اساس شماره موبایل
        /// </summary>
        public User GetByPhoneNumber(string phoneNumber)
        {
            return _context.Users.FirstOrDefault(u => u.mobileNumber == phoneNumber);
        }

        /// <summary>
        /// اضافه کردن کاربر جدید
        /// </summary>
        public void Add(User user)
        {
            _context.Users.Add(user);
            Save();
        }

        /// <summary>
        /// ذخیره تغییرات
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}