using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Services
{
    public class DbManager : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;database=swp;user=root;password=root",
                new MySqlServerVersion(new Version(8, 0, 21))
            );
        }
    }
}
