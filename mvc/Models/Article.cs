using Microsoft.EntityFrameworkCore;

namespace mvc.Models
{
    public enum ArticleType
    {
        Handy,
        Laptop,
        Pc,
    }

    public class Article
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ArticleType ArticleType { get; set; }
    }
}
