using Microsoft.EntityFrameworkCore;

namespace GroupTherapyWebAppFinal.Models
{
    public class PetPalContext : DbContext
    {
        public DbSet<UserModel> Users {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=\Database.db");
    }
}
