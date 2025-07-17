using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class AuthenticationService
    {
        private readonly UserTableRepository _userRepo;

        public AuthenticationService(UserTableRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public UserTable? Login(string phoneNumber, string password)
        {
            return _userRepo.AuthenticateUser(phoneNumber, password);
        }


    }
}
