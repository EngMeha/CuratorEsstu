using Microsoft.AspNetCore.Mvc;

namespace CuratorEsstu.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
