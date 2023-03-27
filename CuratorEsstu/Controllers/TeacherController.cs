using Microsoft.AspNetCore.Mvc;

namespace CuratorEsstu.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
