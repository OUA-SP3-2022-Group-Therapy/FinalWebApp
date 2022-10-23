using $safeprojectname$.Models;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Family_Group()
        {
            return View();
        }

        public IActionResult Member_Profile()
        {
            return View();
        }
    }
}
