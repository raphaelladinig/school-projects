using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers
{
    public class UserController : Controller
    {
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
                using (DbManager db = new DbManager())
                {
                    user.Password = Util.HashPassword(user);
                    db.Users.Add(user);
                    try
                    {
                        await db.SaveChangesAsync();
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
                }
                ;
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
