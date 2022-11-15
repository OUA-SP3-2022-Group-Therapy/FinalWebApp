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
        private readonly UserManager<IdentityUser> UserManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> UserManager)
        {
            _logger = logger;
            this.UserManager = UserManager;
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
        public async Task<IActionResult> Index(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                var result = await UserManager.CheckPasswordAsync(user, model.Password);

                if (result)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
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
