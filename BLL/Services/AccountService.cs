using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepo;

        public AccountService()
        {
            _accountRepo = new AccountRepository();
        }

        public async Task<List<UserTable>> GetAllUsersAsync()
        {
            return await _accountRepo.GetAllUsersAsync();
        }

        public async Task<List<RoleUser>> GetAllRolesAsync()
        {
            return await _accountRepo.GetAllRolesAsync();
        }

        public async Task SaveUsersAsync(IEnumerable<UserTable> users)
        {
            foreach (var user in users)
            {
                await _accountRepo.AddOrUpdateUserAsync(user);
            }
            await _accountRepo.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserTable user)
        {
            _accountRepo.DeleteUser(user);
            await _accountRepo.SaveChangesAsync();
        }
    }
}
