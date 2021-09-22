using System;
using System.Collections.Generic;
using System.Text;

namespace KingFashionShop.Domain.Response.Categories
{
    public class CategoryRespone
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        //public IEnumerable<Product> Products { get; set; }
    }
}
