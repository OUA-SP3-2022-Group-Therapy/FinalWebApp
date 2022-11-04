using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using GroupTherapyWebAppFinal.Models;
using GroupTherapyWebAppFinal.Data;
using System.Security.Policy;
using System.Runtime.ConstrainedExecution;
using System.Data.Entity.ModelConfiguration.Conventions;

// This is the context file used to difine all the models and structure of the database - Joshua Wagner

namespace GroupTherapyWebAppFinal.Data
{    
    public class ApplicationDbContext : DbContext
    {
        // References to models - Joshua Wagner
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<FamilyGroup> FamilyGroup { get; set; }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<Trends> Trends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Extra rules Relational keys are a WIP - Joshua Wagner
            modelBuilder.Entity<Membership>()
                .HasKey(c => new { c.UserID, c.GroupID });
            modelBuilder.Entity<Event>()
                .HasKey(c => new { c.ScheduleID, c.EventName });
            modelBuilder.Entity<Trends>()
                .HasKey(c => new { c.PetID, c.Date });
        }

        //References the database - Joshua Wagner
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Database.db");
    }
}