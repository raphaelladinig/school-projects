using Microsoft.AspNetCore.Mvc;

namespace mvc.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
