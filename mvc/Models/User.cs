using Microsoft.EntityFrameworkCore;

namespace mvc.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
