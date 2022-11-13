using GroupTherapyWebAppFinal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Used for navigation and commands. Home controller WIP - Joshua Wagner
namespace GroupTherapyWebAppFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }        

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        public IActionResult Family_Group()
        {
            return View();
        }

        [Authorize]
        public IActionResult Member_Profile()
        {
            return View();
        }
        
        [Authorize]
        public IActionResult Pet_Profile()
        {
            return View();
        }

    }
}
