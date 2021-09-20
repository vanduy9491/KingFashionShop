using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.ProductRespones
{
    public class CreateProduct
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public int Type { get; set; }
        public string SKU { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public bool Shop { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string Photo { get; set; }
    }
}
