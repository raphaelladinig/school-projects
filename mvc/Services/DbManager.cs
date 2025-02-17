using Microsoft.EntityFrameworkCore;
using mvc.Models;

namespace mvc.Services
{
    public class DbManager : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          // TODO: kp wie des auf linux is
        }
    }
}
