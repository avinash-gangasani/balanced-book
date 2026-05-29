using BalancedBook.NET.Enums;

namespace BalancedBook.NET.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public OrderType Type { get; set; }
    }
}
