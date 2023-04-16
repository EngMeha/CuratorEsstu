using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CuratorEsstu.Controllers
{
    [Authorize(Roles = "Куратор")]
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
