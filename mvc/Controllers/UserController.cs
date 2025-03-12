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
            if (!Util.ValidString(user.Firstname, 2))
            {
                ModelState.AddModelError("Firstname", "Firstname is too short");
            }

            if (!Util.ValidString(user.Lastname, 2))
            {
                ModelState.AddModelError("Lastname", "Lastname is too short");
            }

            if (!Util.ValidString(user.Username, 2))
            {
                ModelState.AddModelError("Username", "Username is too short");
            }

            if (!Util.ValidDate(user.Birthdate))
            {
                ModelState.AddModelError("Date", "Date is in the future");
            }

            if (!Util.ValidPassword(user.Password, 2, 8))
            {
                ModelState.AddModelError("Password", "Invalides passwort");
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (!Util.ValidString(login.Username, 2))
            {
                ModelState.AddModelError("Username", "Username is too short");
            }

            if (!Util.ValidPassword(login.Password, 2, 8))
            {
                ModelState.AddModelError("Password", "Invalides passwort");
            }

            if (ModelState.IsValid)
            {
                var existingUser = _dbManager.Users.FirstOrDefault(u => u.Username == login.Username);

                if (existingUser != null)
                {
                    var result = passwordHasher.VerifyHashedPassword(
                        login.Username,
                        existingUser.Password,
                        login.Password
                    );

                    if (result == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("Username", login.Username);
                        return View(
                            "Message",
                            new Message() { Title = "Erfolg", Text = "Erfolgreicher Login" }
                        );
                    }
                    else
                    {
                        return View(
                            "Message",
                            new Message() { Title = "Fehler", Text = "Falsches Passwort" }
                        );
                    }
                }
                else
                {
                    return View(
                        "Message",
                        new Message() { Title = "Fehler", Text = "User existiert nicht" }
                    );
                }
            }

            return View(login);
        }
        
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}
