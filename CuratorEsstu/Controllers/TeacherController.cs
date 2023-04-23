using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using DataLayer.Entity;
using PresentationLayer;
using InformationParser;

namespace CuratorEsstu.Controllers
{
    [Authorize(Roles = "Куратор")]
    public class TeacherController : Controller
    {
        DataManager _dataManager;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        ServiceManager _serviceManager;
        public TeacherController(DataManager dataManager, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        public static class GlobalTeacher
        {
            public static User Teacher { get; set; }
        }

        public async Task<IActionResult> CreateUser()
        {
            GlobalTeacher.Teacher = await _userManager.GetUserAsync(User);
            return RedirectToAction("Index", "Teacher");
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> EventCity()
        {
            ParserWorker parserWorker = new ParserWorker();
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Autorization");
        }
    }
}
