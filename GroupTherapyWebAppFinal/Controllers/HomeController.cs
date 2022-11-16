using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public ActionResult Error()
        {
            return Content("Email and/or password are invalid");
        }

        public partial interface IServiceProviderIsService
        {
            bool IsService(Type serviceType);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessLogin(UserModel usermodel)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(usermodel))
            {
                return View("Dashboard", "Home");
            }
            else
            {
                return Error();
            }
            
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
