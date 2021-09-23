<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
=======
﻿
using Microsoft.AspNetCore.Http;
>>>>>>> 8e904dea86b68b2f5ab030b54b3c065e7b53e08f
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingFashion.Models.Products
{
    public class UpdateProduct
    {
        public int Id { get; set; }
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

        public List<IFormFile> Photo { get; set; }
        public string ExistPhoto { get; set; }


    }
}
