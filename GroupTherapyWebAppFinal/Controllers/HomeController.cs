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
        //Readonly defs - Joshua Wagner
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
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

        //Shows signup page when required - Joshua Wagner
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        //Verifys the user's identity with the input fields - Joshua Wagner
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

                return RedirectToAction("Dashboard", "Home", new { UserID = user.UserModelID });
            }
            else
            {
                return NotFound();
            }

        }

        //Fetches the dashboard once the user logs in - Joshua Wagner
        [HttpGet]
        public IActionResult Dashboard(int UserID)
        {            
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            List<Membership> Fam = dashboard.FetchFamID(UserID);

            List<UserModel> m = new List<UserModel>();
            foreach(var mem in Fam)
            {
                List<UserModel> Members = dashboard.FetchAllFam(mem.FamilyGroupID);
                m.AddRange(Members);
            }
            
            ViewData["Members"] = m;
            return View(user);
        }


        //Gets the information to show on the member profile - Joshua Wagner
        [HttpGet]
        public IActionResult Member_Profile(int UserID, int ViewID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO member = new UsersDAO();
            UserModel user = member.FetchOne(UserID);
            UserModel viewuser = member.FetchOne(ViewID);
            ViewData["ViewUser"] = viewuser;

            DashboardDAO dashboard = new DashboardDAO();
            List<Membership> Fam = dashboard.FetchFamID(ViewID);
            //int FamID = Fam.FamilyGroupID;

            List<FamilyGroup> f = new List<FamilyGroup>();
            foreach(var mem in Fam)
            {
                List<FamilyGroup> family = dashboard.FetchFamDetails(mem.FamilyGroupID);
                f.AddRange(family);                
            }           

            ViewData["Family"] = f;

            //Add for each statement
            List<Pet> p = new List<Pet>();
            foreach(var mem in Fam)
            {
                List<Pet> pet = dashboard.FetchPetDetails(mem.FamilyGroupID);
                p.AddRange(pet);
            }
            
            ViewData["Pet"] = p;

            return View(user);
        }

        //Gets the information for the family group page to be displayed - Joshua Wagner
        [HttpGet]
        public IActionResult Family_Group(int UserID, int GroupID)
        {
            if (GroupID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            List<UserModel> Members = dashboard.FetchAllFam(GroupID);
            ViewData["Members"] = Members;

            List<UserModel> admin = dashboard.FetchAdmin(GroupID);
            ViewData["Admin"] = admin;

            List<Pet> pet = dashboard.FetchPetDetails(GroupID);
            ViewData["Pet"] = pet;

            return View(user);
        }

        //Gets the information for the pet page to be displayed - Joshua Wagner
        [HttpGet]
        public IActionResult Pet_Profile(int UserID, int PetID)
        {
            if (PetID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            Pet pet = dashboard.FetchPet(PetID);
            ViewData["Pet"] = pet;

            List<FamilyGroup> family = dashboard.FetchFamDetails(pet.FamilyGroupID);
            ViewData["Family"] = family;

            List<Pet> siblings = dashboard.FetchSiblings(PetID, pet.FamilyGroupID);
            ViewData["Siblings"] = siblings;

            List<Trend> trend = dashboard.FetchTrends(PetID);
            ViewData["Trends"] = trend;

            return View(user);
        }

        [HttpGet]
        public IActionResult Schedule(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            DashboardDAO dashboard = new DashboardDAO();

            return null;
        }

    }
}
