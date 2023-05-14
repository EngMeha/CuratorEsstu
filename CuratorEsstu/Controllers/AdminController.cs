using BusinessLayer;
using DataLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PresentationLayer;
using PresentationLayer.Models;

namespace CuratorEsstu.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _singInManager;
        ServiceManager _serviceManager;
        DataManager _dataManager;
        public AdminController(UserManager<User> userManager, SignInManager<User> singInManager
                                , DataManager dataManager) 
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(_dataManager);
        }

        public async Task<IActionResult> AllEmployee() => View(await _serviceManager.UserService.ViewAllEmployee(_userManager));
        
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _dataManager.Users.DeleteUser(id);
            return RedirectToAction("AllEmployee", "Admin");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateEmployee(CreateEmployeeModel model) 
        {
            if(ModelState.IsValid)
            {
                string role = "";
                User user = new User
                {
                    FirstName = model.FirsName,
                    SecondName = model.SecondName,
                    LastName = model.LastName,
                    UserName = model.Login,
                    Email = model.Email,
                    PhoneNumber = model.Phone
                };
                switch (model.Positiion)
                {
                    case 1:
                        role = "Администратор";
                        break;
                    case 2:
                        role = "Куратор";
                        break;
                }
                await _dataManager.Users.CreateUser(user, role, model.Password);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Login", "Authorization");
        }
    }
}
