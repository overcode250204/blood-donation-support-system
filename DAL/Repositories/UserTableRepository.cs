using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserTableRepository
    {
        private readonly BlooddonationsupportsystemContext _context;

        public UserTableRepository(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }

        public UserTable? AuthenticateUser(string phoneNumber, string password)
        {
            return _context.UserTables.FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.PasswordHash == password);
        }

        public void UpdateUser(UserTable user)
        {
            _context.UserTables.Update(user);
            _context.SaveChanges();
        }
    }
}
