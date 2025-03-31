using iTextSharp.text;
using iTextSharp.text.pdf;
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
                .Orders.Where(c => c.Username == username && c.IsCart)
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

                _dbManager.Orders.Add(
                    new Order
                    {
                        Username = username,
                        Article = article,
                        Quantity = quantity,
                        IsCart = true,
                    }
                );

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
                var orders = _dbManager.Orders.Where(c => c.Username == username && c.IsCart);
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

        [HttpPost]
        public async Task<IActionResult> Order()
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                var orders = _dbManager
                    .Orders.Where(c => c.Username == username)
                    .Include(c => c.Article)
                    .ToList();

                foreach (var order in orders)
                {
                    order.IsCart = false;
                }
                await _dbManager.SaveChangesAsync();

                byte[] pdf;
                using (var ms = new MemoryStream())
                {
                    var doc = new Document();
                    var writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    var font = FontFactory.GetFont("Arial", 12);
                    var paragraph = new Paragraph("Bestellung von " + username, font);
                    doc.Add(paragraph);

                    foreach (var order in orders)
                    {
                        if (order == null || order.Article == null)
                        {
                            continue;
                        }

                        paragraph = new Paragraph("Produkt: " + order.Article.Name, font);
                        doc.Add(paragraph);
                        paragraph = new Paragraph("Menge: " + order.Quantity.ToString(), font);
                        doc.Add(paragraph);
                        paragraph = new Paragraph("Preis: " + order.Article.Price.ToString(), font);
                        doc.Add(paragraph);
                        paragraph = new Paragraph(" ", font);
                        doc.Add(paragraph);
                    }

                    doc.Close();
                    writer.Close();

                    pdf = ms.ToArray();
                }

                return File(pdf, "application/pdf", "Bestellung.pdf");
            }
            catch (Exception ex)
            {
                return View("Message", new Message() { Title = "Fehler", Text = ex.Message });
            }
        }
    }
}
