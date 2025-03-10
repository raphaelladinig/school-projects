using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly DbManager _dbManager;
        private readonly PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        public UserController(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            if (user is null)
            {
                return RedirectToAction("Registration");
            }

            if (!Util.ValidString(user.Firstname, 2))
            {
                ModelState.AddModelError("Firstname", "Firstname is too short");
            }

            if (!Util.ValidString(user.Lastname, 2))
            {
                ModelState.AddModelError("Lastname", "Lastname is too short");
            }

            if (!Util.ValidDate(user.Birthdate))
            {
                ModelState.AddModelError("Date", "Date is in the future");
            }

            if (!Util.ValidPassword(user.Password, 2, 8))
            {
                ModelState.AddModelError("Password", "Password is wrong");
            }

            if (ModelState.IsValid)
            {
                user.Password = Util.HashPassword(passwordHasher, user.Password);
                _dbManager.Users.Add(user);
                try
                {
                    await _dbManager.SaveChangesAsync();
                    return View(
                        "Message",
                        new Message()
                        {
                            Title = "Erfolg",
                            Text = "Der User wurde erfolgreich angelegt",
                        }
                    );
                }
                catch
                {
                    return View(
                        "Message",
                        new Message()
                        {
                            Title = "Fehler",
                            Text = "Der User konnte nicht angelegt werden",
                        }
                    );
                }
                ;
            }

            return View(user);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            return View(user);
        }
    }
}
