using Microsoft.AspNetCore.Mvc;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Imprint()
        {
            return View();
        }
    }
}
