using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
        public async Task<IActionResult> CreateUser([Bind("UserModelID,Email,Password,Name,UserType,Gender,DateCreated")] UserModel userModel)
        {
            //return Content(userModel.Gender.ToString());
            _context.Add(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Add_Family_Group");
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
            //return Content(schedules.ElementAt(0).ScheduleName);
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
        public IActionResult Schedule_Profile(int UserID, int FamilyID, int ScheduleID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            UsersDAO users = new UsersDAO();
            UserModel user = users.FetchOne(UserID);

            DashboardDAO dashboard = new DashboardDAO();
            Schedule schedule = dashboard.FetchSchedule(ScheduleID);
            ViewData["ScheduleDet"] = schedule;
            ViewBag.UserID = UserID;
            ViewBag.FamID = FamilyID; 
            ViewBag.ScheduleID = ScheduleID;

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
        public IActionResult Add_Member(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            return View();
        }

        [HttpGet]
        public IActionResult Add_Pet(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            return View();
        }        

        [HttpGet]
        public IActionResult Add_Family_Group(int UserID)
        {
            ViewBag.UserID = UserID;
            return View();
        }

        [HttpGet]
        public IActionResult Add_Trend(int UserID, int PetID)
        {
            ViewBag.UserID = UserID;
            ViewBag.PetID = PetID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrend([Bind("PetID,Date,Height,Weight")] Trend trend, int UserID, int PetID)
        {
            try
            {   
                _context.Add(trend);
                await _context.SaveChangesAsync();
                return RedirectToAction("Pet_Profile", new { UserID, PetID });
                
            }
            catch (Exception e)
            {
                return NotFound(e.Source);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddFamilyGroup([Bind("FamilyGroupID,FamilyName,DateCreated,MemberStatus")] FamilyGroup familyGroup, int UserID)
        {
            try
            {
                _context.Add(familyGroup);
                await _context.SaveChangesAsync();

                DashboardDAO dashboard = new DashboardDAO();
                FamilyGroup family = dashboard.FetchNewGroup();                
                Membership membership = new Membership();
                membership.UserModelID = UserID;
                membership.FamilyGroupID = family.FamilyGroupID;
                membership.IsAdmin = 1;
                _context.Add(membership);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return NotFound(e.Source);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([Bind("UserModelID,FamilyGroupID,IsAdmin")] Membership membership, int UserID, int GroupID)
        {
            try
            { 
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction("Family_Group", new { UserID, GroupID });
            }
            catch (Exception e)
            {
                return NotFound(e.Source);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPet([Bind("PetID,Name,Species,Breed,DOB,Allergies,FamilyGroupID")] Pet pet, int UserID, int GroupID)
        {
            try
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Family_Group", new { UserID, GroupID });
            }
            catch(Exception e)
            {
                return NotFound(e.Source);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add_Admin(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            var membership = await _context.Memberships.FindAsync(UserID, GroupID);
            return View(membership);
        }

        [HttpGet]
        public async Task<IActionResult> Delete_Admin(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            var membership = await _context.Memberships.FindAsync(UserID, GroupID);
            return View(membership);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAdminStatus([Bind("UserModelID,FamilyGroupID,IsAdmin")] Membership membership, int UserID, int GroupID)
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

        [HttpGet]
        public IActionResult Create_Schedule(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([Bind("ScheduleID,ScheduleName,StartDateTime,EndDateTime,ScheduleType,Frequency,Dose,Description,FamilyGroupID")] Schedule schedule, int UserID, int GroupID)
        {
            try
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction("Family_Group", new {UserID, GroupID});
            }
            catch (Exception e)
            {
                return NotFound(e.Source);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit_Schedule(int UserID, int GroupID, int ScheduleID)
        {
            var schedule = await _context.Schedules.FindAsync(ScheduleID);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;
            return View(schedule);
        }

        public async Task<IActionResult> EditSchedule([Bind("ScheduleID,ScheduleName,StartDateTime,EndDateTime,ScheduleType,Frequency,Dose,Description,FamilyGroupID")] Schedule schedule, int UserID, int GroupID)
        {
            if (UserID == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
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
        
        [HttpGet]
        public async Task<IActionResult> Delete_Member(int UserID, int GroupID)
        {
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete_Pet(int UserID, int GroupID)
        {            
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete_Schedule(int UserID, int GroupID)
        {
            
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete_Family_Group(int UserID, int GroupID)
        {
            
            ViewBag.UserID = UserID;
            ViewBag.GroupID = GroupID;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFamGroup(int FamID)
        {
            if (_context.FamilyGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FamilyGroups'  is null.");
            }
            var familyGroup = await _context.FamilyGroups.FindAsync(FamID);
            if (familyGroup != null)
            {
                _context.FamilyGroups.Remove(familyGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePet(int PetID)
        {
            if (_context.Pets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pets'  is null.");
            }
            var pet = await _context.Pets.FindAsync(PetID);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMember(int UserModelID)
        {
            if (_context.Memberships == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Members'  is null.");
            }
            var membership = await _context.Memberships.FindAsync(UserModelID);
            if (membership != null)
            {
                _context.Memberships.Remove(membership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchedule(int ScheduleID)
        {
            if (_context.Memberships == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(ScheduleID);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
