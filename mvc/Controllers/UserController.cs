using Microsoft.AspNetCore.Mvc;
using mvc.Models;

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

            if (!Util.ValidString(user.Lastname, 2))
            {
                ModelState.AddModelError("Lastname", "Lastname is too short");
            }

            if (!Util.ValidDate(user.Birthdate))
            {
                ModelState.AddModelError("Date", "Date is in the future");
            }

            if (!Util.ValidPassword(user.Password, 8, 1, 1, 1, 2, "!@#$%&/()=?"))
            {
                ModelState.AddModelError("Password", "Password is too short");
            }

            if (ModelState.IsValid)
            {
                //in DB eintragen
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
