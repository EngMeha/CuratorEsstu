using BusinessLayer;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace CuratorEsstu.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataManager _dataManager;

        public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager
                                        ,DataManager dataManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataManager = dataManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new AuthorizationModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthorizationModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(model.Login));
                    switch (role[0])
                    {
                        case "Администратор":
                            return RedirectToAction("Index", "Admin");
                        case "Куратор":
                            return RedirectToAction("CreateUser", "Teacher");
                    }
                }
            }
            return View(model);
        }
    }
}
