using BLL.Interfaces;
using DAL.DataAccess;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsersService : IUsers
    {
        private readonly AppDataContext _context;

        public UsersService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Users> AddUsersAsync(Users users)
        {
            await _context.User.AddAsync(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<bool> DeleteUsersAsync(int id)
        {
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser != null)
            {
                _context.User.Remove(existingUser);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<Users> GetUsersByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<Users> UpdateUsersAsync(Users users)
        {
            var existingUser = await _context.User.FindAsync(users.UserID);
            if (existingUser != null)
            {
                existingUser.Username = users.Username;
                existingUser.Email = users.Email;
                existingUser.Password = users.Password;
                existingUser.Role = users.Role;

                _context.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            return existingUser ?? throw new KeyNotFoundException("User not found.");
        }
    }
}
