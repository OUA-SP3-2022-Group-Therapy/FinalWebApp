using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Data;
using System.Security.Policy;
using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

// This is the context file used to difine all the models and structure of the database - Joshua Wagner

namespace GroupTherapyWebAppFinal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        // References to models - Joshua Wagner
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<FamilyGroup> FamilyGroups { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Trend> Trends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Extra rules are a WIP - Joshua Wagner
            modelBuilder.Entity<Membership>()
                .HasKey(c => new { c.UserModelID, c.FamilyGroupID });
            modelBuilder.Entity<Event>()
                .HasKey(c => new { c.ScheduleID, c.EventName });
            modelBuilder.Entity<Trend>()
                .HasKey(c => new { c.PetID, c.Date });
        }

        //References and establishes connection to the database - Joshua Wagner
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}