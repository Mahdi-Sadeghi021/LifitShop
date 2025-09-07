using DataAccess.Models;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// گرفتن کاربر بر اساس شماره موبایل
        /// </summary>
        User GetByPhoneNumber(string phoneNumber);

        /// <summary>
        /// اضافه کردن کاربر جدید
        /// </summary>
        void Add(User user);

        /// <summary>
        /// ذخیره تغییرات در دیتابیس
        /// </summary>
        void Save();
    }
}