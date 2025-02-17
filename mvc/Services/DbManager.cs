using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Services
{
    public class DbManager : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=mvc;User=root;");
            }
        }
    }
}
