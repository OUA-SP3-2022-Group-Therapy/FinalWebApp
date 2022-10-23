using GroupTherapyApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupTherapyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult index()
        {
            return View();
        }

        public IActionResult dashboard()
        {
            return View();
        }

        public IActionResult family_Group()
        {
            return View();
        }

        public IActionResult member_profile()
        {
            return View();
        }
    }
}
