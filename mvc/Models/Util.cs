using Microsoft.AspNetCore.Identity;

namespace mvc.Models
{
    public class Util
    {
        public static bool ValidString(string input, int minLength = 0, int maxLength = 0)
        {
            if (input == null)
            {
                return false;
            }

            if (minLength > 0 && input.Length < minLength)
            {
                return false;
            }

            if (maxLength > 0 && input.Length > maxLength)
            {
                return false;
            }

            return true;
        }

        public static bool ValidDate(DateTime input)
        {
            if (input > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public static bool ValidPassword(string input, int minLength = 0, int maxLength = 0)
        {
            if (input == null)
            {
                return false;
            }

            if (minLength > 0 && input.Length < minLength)
            {
                return false;
            }

            if (maxLength > 0 && input.Length > maxLength)
            {
                return false;
            }

            return true;
        }

        public static string HashPassword(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, user.Password);
        }
    }
}
