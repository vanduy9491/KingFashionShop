using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string SKU { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Content { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
