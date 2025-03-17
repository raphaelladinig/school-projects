using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc.Models;
using mvc.Services;

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

        public IActionResult Cart()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return View(
                    "Message",
                    new Message()
                    {
                        Title = "Fehler",
                        Text = "Bitte einloggen, um den Warenkorb anzuzeigen.",
                    }
                );
            }

            var cartItems = _dbManager
                .Orders.Where(c => c.Username == username)
                .Include(c => c.Article)
                .ToList();

            return View(cartItems);
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
                try
                {
                    _dbManager.Articles.Add(article);
                    await _dbManager.SaveChangesAsync();
                    return View("Message", new Message() { Title = "Erfolg", Text = "" });
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int articleId, int quantity)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return View(
                    "Message",
                    new Message()
                    {
                        Title = "Fehler",
                        Text = "Bitte einloggen, um Artikel zum Warenkorb hinzuzufügen.",
                    }
                );
            }

            var article = await _dbManager.Articles.FindAsync(articleId);
            if (article == null)
            {
                return View(
                    "Message",
                    new Message() { Title = "Fehler", Text = "Artikel nicht gefunden." }
                );
            }

            try
            {
                var cartItem = _dbManager.Orders.FirstOrDefault(c =>
                    c.Username == username && c.Article.ArticleId == articleId
                );

                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    _dbManager.Orders.Add(
                        new Order
                        {
                            Username = username,
                            Article = article,
                            Quantity = quantity,
                        }
                    );
                }

                await _dbManager.SaveChangesAsync();
                return View("Message", new Message() { Title = "Erfolg", Text = "" });
            }
            catch
            {
                return View("Message", new Message() { Title = "Fehler", Text = "" });
            }
            ;
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                var orders = _dbManager.Orders.Where(c => c.Username == username);
                _dbManager.Orders.RemoveRange(orders);
                await _dbManager.SaveChangesAsync();
                return View("Message", new Message() { Title = "Erfolg", Text = "" });
            }
            catch
            {
                return View("Message", new Message() { Title = "Fehler", Text = "" });
            }
            ;
        }
    }
}
