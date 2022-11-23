using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
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

        //Create a new user task - Joshua Wagner
        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserModelID,Email,Password,Name,UserType,Gender,DateCreated")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View("Index","Home");
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

            List<FamilyGroup> f = new List<FamilyGroup>();
            foreach (var mem in Fam)
            {
                List<FamilyGroup> family = dashboard.FetchFamDetails(mem.FamilyGroupID);
                f.AddRange(family);
            }

            ViewData["Family"] = f;

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

            List<FamilyGroup> f = new List<FamilyGroup>();
            foreach(var mem in Fam)
            {
                List<FamilyGroup> family = dashboard.FetchFamDetails(mem.FamilyGroupID);
                f.AddRange(family);                
            }           

            ViewData["Family"] = f;

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

            List<Schedule> schedules = dashboard.FetchSchedules(GroupID);
            ViewData["Schedules"] = schedules;

            ViewBag.FamID = GroupID;

            List <Membership> Fam = dashboard.FetchFamID(UserID);
            List<FamilyGroup> f = new List<FamilyGroup>();
            foreach (var mem in Fam)
            {
                List<FamilyGroup> family = dashboard.FetchFamDetails(mem.FamilyGroupID);
                f.AddRange(family);
            }

            ViewData["Family"] = f;

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
            int FamID = pet.FamilyGroupID;
            ViewData["Pet"] = pet;

            List<FamilyGroup> family = dashboard.FetchFamDetails(FamID);
            ViewData["Family"] = family;

            List<Pet> siblings = dashboard.FetchSiblings(PetID, FamID);
            ViewData["Siblings"] = siblings;

            List<Trend> trend = dashboard.FetchTrends(PetID);
            ViewData["Trends"] = trend;

            return View(user);
        }

        [HttpGet]
        public IActionResult Schedule_Profile(int UserID, int FamilyID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            Schedule schedule = dashboard.FetchSchedule(FamilyID);
            ViewData["ScheduleDet"] = schedule;

            return View(user);
        }

        [HttpGet]
        public IActionResult All_Pets(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            List<Membership> Fam = dashboard.FetchFamID(UserID);

            List<Pet> p = new List<Pet>();
            foreach (var mem in Fam)
            {
                List<Pet> pet = dashboard.FetchPetDetails(mem.FamilyGroupID);
                p.AddRange(pet);
            }

            ViewData["Pet"] = p;

            return View(user);
        }

        [HttpGet]
        public IActionResult All_Fam_Groups(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            List<Membership> Fam = dashboard.FetchFamID(UserID);

            List<FamilyGroup> f = new List<FamilyGroup>();
            foreach (var mem in Fam)
            {
                List<FamilyGroup> family = dashboard.FetchFamDetails(mem.FamilyGroupID);
                f.AddRange(family);
            }

            ViewData["Family"] = f;

            return View(user);
        }

        [HttpGet]
        public IActionResult All_Schedules(int UserID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            List<Membership> Fam = dashboard.FetchFamID(UserID);

            List<Schedule> s = new List<Schedule>();
            foreach (var item in Fam)
            {
                List<Schedule> schedules = dashboard.FetchSchedules(item.FamilyGroupID);
                s.AddRange(schedules);
            }

            ViewData["Schedule"] = s;

            return View(user);
        }

        [HttpGet]
        public IActionResult Add_Admin(int GroupID)
        {
            ViewBag.GroupID = GroupID;
            return View();
        }

        [HttpGet]
        public IActionResult Add_User(int GroupID)
        {
            ViewBag.GroupID = GroupID;
            return View();
        }

        [HttpGet]
        public IActionResult Add_Pet(int GroupID)
        {
            ViewBag.GroupID = GroupID;
            return View();
        }

        //To be implemented
        [HttpPost]
        public async Task<IActionResult> ChangeAdminStatus(int UserID, int GroupID, [Bind("UserModelID,FamilyGroupID,IsAdmin")] Membership membership)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Family_Group", new { UserID, GroupID });
            }
            return RedirectToAction("Family_Group", new { UserID, GroupID });
        }

        //To be implemented
        [HttpPost]
        public async Task<IActionResult> AddFamUser(int UserID, int GroupID, [Bind("UserModelID,FamilyGroupID,IsAdmin")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction("Family_Group", new { UserID, GroupID });
            }
            return RedirectToAction("Family_Group", new { UserID, GroupID });
        }

        //To be implemented
        [HttpPost]
        public async Task<IActionResult> AddPet(int UserID, int GroupID, [Bind("UserModelID,FamilyGroupID,IsAdmin")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Family_Group", new { UserID, GroupID });
            }
            return RedirectToAction("Family_Group", new { UserID, GroupID });
        }
    }
}
