using GroupTherapyWebAppFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupTherapyWebAppFinal.Data
{
    public class UserContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=\Data\GroupTherapy.db");
    }
}
