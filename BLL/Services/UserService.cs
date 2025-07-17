using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserTableRepository _userRepo;
        public UserService(UserTableRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public void UpdateUser(UserTable user)
        {
            _userRepo.UpdateUser(user);
        }
    }
} 