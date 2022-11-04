using Microsoft.AspNetCore.Mvc;

// Used for navigation and commands. Home controller WIP - Joshua Wagner
namespace GroupTherapyWebAppFinal.Controllers
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

        public IActionResult Pet_Profile()
        {
            return View();
        }
    }
}
