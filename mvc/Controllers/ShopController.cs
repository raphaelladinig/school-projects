using Microsoft.AspNetCore.Mvc;
using mvc.Services;
using mvc.Models;

namespace mvc.Controllers
{
    public class ShopController : Controller
    {
        private readonly DbManager _dbManager;

        public ShopController(DbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public IActionResult Index()
        {
            var articles = _dbManager.Articles.ToList();
            return View(articles);
        }

        public IActionResult Admin()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == "admin")
            {
                return View();
            }
            else
            {
                return View(
                    "Message",
                    new Message() { Title = "Fehler", Text = "Nur der Admin hat zugriff" }
                );
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article)
        {
            if (!Util.ValidString(article.Name, 2))
            {
                ModelState.AddModelError("Name", "Name is too short");
            }
            
            if (!Util.ValidString(article.Description, 2))
            {
                ModelState.AddModelError("Description", "Description is too short");
            }

            if (ModelState.IsValid)
            {
                _dbManager.Articles.Add(article);
                try
                {
                    await _dbManager.SaveChangesAsync();
                    return View(
                        "Message",
                        new Message()
                        {
                            Title = "Erfolg",
                            Text = "Der Artikel wurde erfolgreich angelegt",
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
                            Text = "Der Artikel konnte nicht angelegt werden",
                        }
                    );
                }
                ;
            }

            return View(article);
        }
    }
}
