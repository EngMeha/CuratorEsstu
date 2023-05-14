using BusinessLayer;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using PresentationLayer.Mappers;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Service
{
    public class UserService
    {
        DataManager _dataManager;
        public UserService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<ViewAllEmployeeModel> ViewAllEmployee(UserManager<User> userManager)
        {
            return AllEmployeeMapper.FromDbToModel(userManager, await _dataManager.Users.GetListUser(true));
        }
    }
}
