using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUsers
    {
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<Users> GetUsersByIdAsync(int id);
        Task<Users> AddUsersAsync(Users users);
        Task<Users> UpdateUsersAsync(Users users);
        Task<bool> DeleteUsersAsync(int id);
    }
}
