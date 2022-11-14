using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Used for navigation and commands. Home controller WIP - Joshua Wagner
namespace GroupTherapyWebAppFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Family_Group()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Member_Profile()
        {
            return View();
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Pet_Profile()
        {
            return View();
        }

    }
}
