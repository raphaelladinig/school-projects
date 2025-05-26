using Microsoft.AspNetCore.Mvc;
using mvc.Services;

namespace mvc.Controllers
{
    public class ChatController : Controller
    {
        private readonly DbManager _dbManager;

        public ChatController(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "User");
            }

            ViewData["username"] = username;
            return View();
        }
    }
}
