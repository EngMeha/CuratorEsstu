using Microsoft.AspNetCore.Mvc;

namespace CuratorEsstu.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
