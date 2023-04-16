using BusinessLayer;
using CuratorEsstu.Models;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Diagnostics;

namespace CuratorEsstu.Controllers
{
    public class HomeController : Controller
    {
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}