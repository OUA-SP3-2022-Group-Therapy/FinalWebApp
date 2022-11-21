using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

// Used for navigation and commands - Joshua Wagner
namespace GroupTherapyWebAppFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                _logger.LogInformation("User logged in.");
                //return Content(user.UserModelID.ToString()); ---> Used for testing ID value. Ignore this.

                return RedirectToAction("Dashboard", "Home", new { UserID = user.UserModelID });
            }
            else
            {
                return NotFound();
            }

        }

        //Fetches user details (WIP) - Joshua Wagner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUserDetails(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO usersFull = new UsersDAO();
            UserModel UserDetails = usersFull.FetchOne(UserID);
            //return View("Member_Profile", "Home");
            return Content(UserID.ToString());
        }

        [HttpGet]
        public IActionResult Dashboard(int UserID)
        {
            //return Content(UserID.ToString()); ---> Used for testing ID value.Ignore this.

            if (UserID == 0)
            {
                return NotFound();
            }

            DashboardDAO dashboard = new DashboardDAO();
            Membership Fam = dashboard.FetchFamID(UserID);
            List<UserModel> Members = new List<UserModel>();
            Members = dashboard.FetchAllFam(Fam.FamilyGroupID);

            return View(Members);
        }


        //Gets the information to show on the member profile (WIP) - Joshua Wagner
        [HttpGet]
        public IActionResult Member_Profile(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO member = new UsersDAO();
            UserModel UserDetails = member.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            Membership Fam = dashboard.FetchFamID(UserID);
            int FamID = Fam.FamilyGroupID;

            List<UserModel> Members = dashboard.FetchOtherFam(UserID, FamID);

            List<UserModel> Formatted = new List<UserModel>
            {
                UserDetails
            };

            Formatted.Concat(Members);

            List<FamilyGroup> family = dashboard.FetchFamDetails(Fam.FamilyGroupID);
            ViewData["Family"] = family;

            List<Pet> pet = dashboard.FetchPetDetails(Fam.FamilyGroupID);
            ViewData["Pet"] = pet;

            return View(Formatted);
        }

        //Gets the information for the family group page to be displayed (WIP) - Joshua Wagner
        [HttpGet]
        public IActionResult Family_Group(int GroupID)
        {
            if (GroupID == 0)
            {
                return NotFound();
            }

            DashboardDAO dashboard = new DashboardDAO();
            List<UserModel> Members = new List<UserModel>();
            Members = dashboard.FetchAllFam(GroupID);

            List<UserModel> admin = dashboard.FetchAdmin(GroupID);
            ViewData["Admin"] = admin;

            List<Pet> pet = dashboard.FetchPetDetails(GroupID);
            ViewData["Pet"] = pet;

            return View(Members);
        }

        //Gets the information for the pet page to be displayed (WIP) - Joshua Wagner
        [HttpGet]
        public IActionResult Pet_Profile(int PetID)
        {
            if (PetID == 0)
            {
                return NotFound();
            }

            DashboardDAO dashboard = new DashboardDAO();
            Pet pet = dashboard.FetchPet(PetID);

            List<FamilyGroup> family = dashboard.FetchFamDetails(pet.FamilyGroupID);
            ViewData["Family"] = family;

            List<Pet> siblings = dashboard.FetchSiblings(PetID, pet.FamilyGroupID);
            ViewData["Siblings"] = siblings;

            return View(pet);
        }
    }
}
