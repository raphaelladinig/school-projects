using Microsoft.EntityFrameworkCore;

namespace mvc.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Username { get; set; }
        public Article Article { get; set; }
        public int Quantity { get; set; }
        public bool IsCart { get; set; } 
    }
}
