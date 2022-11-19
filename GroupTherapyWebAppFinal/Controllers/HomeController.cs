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

        public static int CurrentUserID;

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
            UsersDAO usersDAO = new UsersDAO();
            bool success = usersDAO.FindUserByEmailAndPassword(usermodel);
            
            if (success == true)
            {
                UserModel user = usersDAO.FetchID(usermodel);
                //int ID = user.UserModelID;
                //ISession["UserID"] = ID;
                _logger.LogInformation("User logged in.");

                //return Content(user.UserModelID.ToString()); ---> Used for testing ID value. Ignore this.

                return RedirectToAction("Dashboard", "Home", new { UserID = user.UserModelID });
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
            UsersDAO usersFull = new UsersDAO();

            UserModel UserDetails = usersFull.FetchOne(UserID);

            ViewData["Member"] = UserDetails;

            //return View("Member_Profile", "Home");
            return Content(UserID.ToString());
        }

        [HttpGet]
        public IActionResult Dashboard(int UserID)
        {
            //return Content(UserID.ToString()); ---> Used for testing ID value.Ignore this.
            DashboardDAO dashboard = new DashboardDAO();
            Membership Fam = dashboard.FetchFamID(UserID);
            int FamID = Fam.FamilyGroupID;
            List<UserModel> Members = new List<UserModel>();
            Members = dashboard.FetchAllFam(FamID);
            //ViewData["Members"] = Members;
            return View(Members);
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
