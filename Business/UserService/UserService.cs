using DataAccess.Models;
using DataAccess.Repositories.UserRepository;

namespace Business.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// گرفتن کاربر بر اساس شماره موبایل
        /// </summary>
        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _userRepository.GetByPhoneNumber(phoneNumber);
        }

        /// <summary>
        /// ثبت‌نام کاربر جدید
        /// </summary>
        public void RegisterUser(string phoneNumber)
        {
            // چک کن شماره قبلا ثبت نشده باشه
            var existingUser = _userRepository.GetByPhoneNumber(phoneNumber);
            if (existingUser != null)
                throw new Exception("این شماره موبایل قبلاً ثبت شده است.");

            var user = new User
            {
                mobileNumber = phoneNumber,
                RegisterDate = DateTime.Now
            };

            _userRepository.Add(user);
        }
    }
}