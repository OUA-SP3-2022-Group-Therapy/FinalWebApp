using Microsoft.AspNetCore.Mvc;

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

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
