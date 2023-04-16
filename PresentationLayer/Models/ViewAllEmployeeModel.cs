using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class ViewAllEmployeeModel
    {
        public UserManager<User> UserManager { get; set; }
        public List<User> Users { get; set; }
    }
}
