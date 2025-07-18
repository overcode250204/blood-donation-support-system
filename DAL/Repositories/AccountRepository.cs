using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class AccountRepository
    {
        private readonly BlooddonationsupportsystemContext _context;

        public AccountRepository()
        {
            _context = new BlooddonationsupportsystemContext();
        }

        public async Task<List<UserTable>> GetAllUsersAsync()
        {
            return await _context.UserTables.Include(u => u.Role).ToListAsync();
        }

        public async Task<UserTable?> GetUserByIdAsync(Guid userId)
        {
            return await _context.UserTables.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<RoleUser>> GetAllRolesAsync()
        {
            return await _context.RoleUsers.ToListAsync();
        }

        public async Task AddOrUpdateUserAsync(UserTable user)
        {
            bool exists = await _context.UserTables.AnyAsync(u => u.UserId == user.UserId);
            if (exists)
            {
                _context.UserTables.Update(user);
            }
            else
            {
                await _context.UserTables.AddAsync(user);
            }
        }

        public void DeleteUser(UserTable user)
        {
            _context.UserTables.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
