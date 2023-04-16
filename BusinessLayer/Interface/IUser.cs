using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUser
    {
        Task CreateUser(User user, string role, string password);
        Task DeleteUser(string id);
        Task UpdateUser(User user);
        Task<User> GetUser(int id, bool include);
        Task<List<User>> GetListUser(bool include);
    }
}
