using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Used for navigation and commands - Joshua Wagner
namespace GroupTherapyWebAppFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public int CurrentUserID { get; set; }

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

        //Verifys the user's identity with the input fields.
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

        //Fetches user details (WIP) - Joshua Wagner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUserDetails(int UserID)
        {
            UsersFullDAO usersFull = new UsersFullDAO();

            UserModel UserDetails = usersFull.FetchOne(UserID);

            return View("Member_Profile", UserDetails);

        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Family_Group()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Member_Profile()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Pet_Profile()
        {
            return View();
        }

    }
}
