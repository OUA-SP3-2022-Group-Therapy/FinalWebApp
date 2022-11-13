using GroupTherapyWebAppFinal.Areas.Identity.Data;
using GroupTherapyWebAppFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;

namespace GroupTherapyWebAppFinal.Data;

public class GroupTherapyWebAppFinalContext : IdentityDbContext<GroupTherapyWebAppFinalUser>
{
    public GroupTherapyWebAppFinalContext(DbContextOptions<GroupTherapyWebAppFinalContext> options)
        : base(options)
    {
    }

    public DbSet<GroupTherapyWebAppFinalUser> GroupTherapyWebAppFinalUsers { get; set; }
    public DbSet<UserModel> UserModels { get; set; }
    public DbSet<FamilyGroup> FamilyGroups { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Trend> Trends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Membership>(b =>
        {
            b.HasKey(c => new { c.UserModelID, c.FamilyGroupID });
        });

        modelBuilder.Entity<Event>(b =>
        {
            b.HasKey(c => new { c.ScheduleID, c.EventName });
        });

        modelBuilder.Entity<Trend>(b =>
        {
            b.HasKey(c => new { c.PetID, c.Date });
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=tcp:petpal-server.database.windows.net,1433;Initial Catalog=PetPalDatabase;Persist Security Info=False;User ID=wagnerj;Password=pet@pal1315;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

}

public class GroupTherapyFinalWebAppFactory : IDesignTimeDbContextFactory<GroupTherapyWebAppFinalContext>
{
    public GroupTherapyWebAppFinalContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GroupTherapyWebAppFinalContext>();
        optionsBuilder.UseSqlServer("GroupTherapyWebAppFinalContextConnection");

        return new GroupTherapyWebAppFinalContext(optionsBuilder.Options);
    }
}