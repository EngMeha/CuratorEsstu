using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Mappers
{
    public static class AllEmployeeMapper
    {
        public static ViewAllEmployeeModel FromDbToModel(UserManager<User> userManager, List<User> users)
        {
            return new ViewAllEmployeeModel() { UserManager = userManager, Users = users};
        }
    }
}
