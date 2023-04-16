using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EFUser:IUser
    {
        DiplomContext _context;
        UserManager<User> _userManager;
        public EFUser(DiplomContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateUser(User user, string role, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }

        public Task<List<User>> GetListUser(bool include)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id, bool include)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
